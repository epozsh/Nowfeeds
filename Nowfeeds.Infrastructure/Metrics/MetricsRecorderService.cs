using Nowfeeds.Infrastructure.Interfaces;
using System.Collections.Concurrent;

namespace Nowfeeds.Infrastructure.Metrics
{
	public class MetricsRecorderService : IMetricsRecorderService
	{
		private readonly ICacheService _cacheService;
		private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

		public MetricsRecorderService(ICacheService cacheService)
		{
			_cacheService = cacheService;
		}

		public async Task<Metric[]> GetMetricsAsync(string[] keys, CancellationToken cancellationToken)
		{
			var tasks = keys.Select(key => _cacheService.GetAsync<Metric>(key, cancellationToken));

			var results = await Task.WhenAll(tasks);

			return results.Where(m => m != null).ToArray();
		}

		public async Task UpdateMetricsAsync(string key, string title, double responseTimeMs, bool success, CancellationToken cancellationToken)
		{
			SemaphoreSlim semaphore = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

			await semaphore.WaitAsync(cancellationToken);

			try
			{
				var metrics = await _cacheService.GetAsync<Metric>(key, cancellationToken);
				if (metrics == null)
				{
					metrics = new Metric
					{
						Title = title,
						TotalRequests = 0,
						TotalResponseTimeMs = 0,
						SuccessCount = 0,
						FailureCount = 0,
						LastRequestUtc = DateTime.UtcNow
					};
				}

				metrics.TotalRequests++;
				metrics.TotalResponseTimeMs += responseTimeMs;
				metrics.LastRequestUtc = DateTime.UtcNow;
				if (success)
					metrics.SuccessCount++;
				else
					metrics.FailureCount++;

				await _cacheService.AddAsync(
					key,
					metrics,
					null,
					TimeSpan.FromHours(1), // TODO: Make configurable
					cancellationToken);
			}
			finally
			{
				semaphore.Release();
			}
		}
	}
}

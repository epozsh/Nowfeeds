using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Metrics
{
	public class MetricsRecorderService : IMetricsRecorderService
	{
		private readonly ICacheService _cacheService;

		public MetricsRecorderService(ICacheService cacheService)
		{
			_cacheService = cacheService;
		}

		public async Task<Metric[]> GetMetricsAsync(string[] keys, CancellationToken cancellationToken)
		{
			var tasks = keys.Select(key => _cacheService.GetAsync<Metric>(key, cancellationToken));
			var results = await Task.WhenAll(tasks);
			// Filter out nulls if a metric is not found in cache
			return results.Where(m => m != null).ToArray();
		}

		public async Task UpdateMetricsAsync(string key, string title, double responseTimeMs, bool success, CancellationToken cancellationToken)
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
	}
}

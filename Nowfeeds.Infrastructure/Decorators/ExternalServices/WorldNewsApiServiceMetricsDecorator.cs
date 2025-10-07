using Microsoft.Extensions.Options;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using System.Diagnostics;

namespace Nowfeeds.Infrastructure.Decorators.ExternalServices
{
	public class WorldNewsApiServiceMetricsDecorator : IWorldNewsApiService
	{
		private readonly IWorldNewsApiService _decorated;
		private readonly IMetricsRecorderService _metricsRecorderService;
		private readonly string _metricsCacheKey;

		public WorldNewsApiServiceMetricsDecorator(IWorldNewsApiService decorated, IMetricsRecorderService metricsRecorderService, IOptionsSnapshot<InfrastructureConfiguration> infrastructureConfiguration)
		{
			_decorated = decorated;
			_metricsRecorderService = metricsRecorderService;
			_metricsCacheKey = infrastructureConfiguration.Value.CacheSettings.WorldNewsApiKey;
		}

		public async Task<WorldNewsApiResponse> SearchNews(string country, string text, CancellationToken cancellationToken)
		{
			var callName = $"{_decorated.GetType()}.{nameof(SearchNews)}";
			var stopwatch = Stopwatch.StartNew();
			bool success = false;
			try
			{
				var result = await _decorated.SearchNews(country, text, cancellationToken);
				success = true;
				return result;
			}
			catch
			{
				success = false;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				await _metricsRecorderService.UpdateMetricsAsync(_metricsCacheKey, callName, stopwatch.Elapsed.TotalMilliseconds, success, cancellationToken);
			}
		}
	}
}

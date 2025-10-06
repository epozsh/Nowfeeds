using Microsoft.Extensions.Options;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class MetricsService : IMetricsService
	{
		private readonly IMetricsRecorderService _metricsRecorderService;
		private readonly CacheSettings _cacheSettings;

		public MetricsService(IMetricsRecorderService metricsRecorderService, IOptionsSnapshot<InfrastructureConfiguration> optionsSnapshot)
		{
			_metricsRecorderService = metricsRecorderService;
			_cacheSettings = optionsSnapshot.Value.CacheSettings;
		}

		public async Task<string> GetMetrics(CancellationToken cancellationToken)
		{
			var metrics = await _metricsRecorderService.GetMetricsAsync([_cacheSettings.OpenWeatherMapKey, _cacheSettings.TwitterKey], cancellationToken);

			var lines = metrics
			   .Where(m => m != null)
			   .Select(m =>
			   $"Title: {m.Title}, " +
			   $"TotalRequests: {m.TotalRequests}, " +
			   $"SuccessCount: {m.SuccessCount}, " +
			   $"FailureCount: {m.FailureCount}, " +
			   $"TotalResponseTimeMs: {m.TotalResponseTimeMs}, " +
			   $"AverageResponseTimeMs: {m.AverageResponseTimeMs}, " +
			   $"LastRequestUtc: {m.LastRequestUtc:O}");

			return string.Join(Environment.NewLine, lines);
		}
	}
}

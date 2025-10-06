using Microsoft.Extensions.Options;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using System.Diagnostics;

namespace Nowfeeds.Infrastructure.Decorators.ExternalServices
{
	public class OpenWeatherMapServiceMetricsDecorator : IOpenWeatherMapService
	{
		private readonly IOpenWeatherMapService _decorated;
		private readonly IMetricsRecorderService _metricsRecorderService;
		private readonly string _metricsCacheKey;

		public OpenWeatherMapServiceMetricsDecorator(IOpenWeatherMapService decorated, IMetricsRecorderService metricsRecorderService, IOptionsSnapshot<InfrastructureConfiguration> infrastructureConfiguration)
		{
			_decorated = decorated;
			_metricsRecorderService = metricsRecorderService;
			_metricsCacheKey = infrastructureConfiguration.Value.CacheSettings.OpenWeatherMapKey;
		}

		public async Task<OpenWeatherMapCurrentApiResponse> GetCurrentWeather(string city, CancellationToken cancellationToken)
		{
			var callName = $"{_decorated.GetType()}.{nameof(GetCurrentWeather)}";
			var stopwatch = Stopwatch.StartNew();
			bool success = false;
			try
			{
				var result = await _decorated.GetCurrentWeather(city, cancellationToken);
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

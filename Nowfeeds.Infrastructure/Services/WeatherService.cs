using Nowfeeds.Application.Interfaces;
using Nowfeeds.Domain.Values;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class WeatherService : IWeatherService
	{
		private readonly IOpenWeatherMap _openWeatherMap;

		public WeatherService(IOpenWeatherMap openWeatherMap)
		{
			_openWeatherMap = openWeatherMap;
		}

		public async Task<Weather> GetWeatherAsync(string city, CancellationToken cancellationToken)
		{
			OpenWeatherMapCurrentApiResponse result = await _openWeatherMap.GetCurrentWeather(city, cancellationToken);

			Weather weather = new Weather
			{
				Description = result.Weather.FirstOrDefault()?.Description ?? string.Empty,
				Temperature = result.Main.Temp,
				Humidity = result.Main.Humidity,
				WindSpeed = result.Wind.Speed
			};

			return weather;
		}
	}
}

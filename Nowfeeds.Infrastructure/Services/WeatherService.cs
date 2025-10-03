using Nowfeeds.Application.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class WeatherService : IWeatherService
	{
		public Task<string> GetWeatherAsync(string location)
		{
			throw new NotImplementedException();
		}
	}
}

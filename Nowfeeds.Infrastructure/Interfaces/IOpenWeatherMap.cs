using Nowfeeds.Infrastructure.ExternalServices.Models;

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface IOpenWeatherMap
	{
		public Task<OpenWeatherMapCurrentApiResponse> GetCurrentWeather(string city, CancellationToken cancellationTokens);
	}
}

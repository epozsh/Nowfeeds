using Nowfeeds.Infrastructure.ExternalServices.Models;

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface IOpenWeatherMapService
	{
		public Task<OpenWeatherMapCurrentApiResponse> GetCurrentWeather(string city, CancellationToken cancellationTokens);
	}
}

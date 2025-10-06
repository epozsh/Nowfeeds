using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Interfaces
{
	public interface IWeatherService
	{
		Task<Weather> GetWeatherAsync(string location, CancellationToken cancellationToken);
	}
}

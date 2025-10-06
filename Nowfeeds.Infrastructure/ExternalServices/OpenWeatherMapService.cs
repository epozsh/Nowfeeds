using Microsoft.Extensions.Options;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using System.Text.Json;

namespace Nowfeeds.Infrastructure.ExternalServices
{
	public class OpenWeatherMapService : IOpenWeatherMapService
	{
		private readonly HttpClient _httpClient;
		private readonly OpenWeatherMapConfiguration _openWeatherMapConfiguration;
		public OpenWeatherMapService(HttpClient httpClient, IOptionsSnapshot<InfrastructureConfiguration> configuration)
		{
			_httpClient = httpClient;
			_openWeatherMapConfiguration = configuration.Value.ExternalServices.OpenWeatherMap;
		}

		public async Task<OpenWeatherMapCurrentApiResponse> GetCurrentWeather(string city, CancellationToken cancellationToken)
		{
			string url = $"{_openWeatherMapConfiguration.BaseUrl}/weather?q={city}&units=metric&appid={_openWeatherMapConfiguration.ApiKey}";
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

			var responseDeserialized = JsonSerializer.Deserialize<OpenWeatherMapCurrentApiResponse>(responseContent);

			return responseDeserialized;
		}
	}
}

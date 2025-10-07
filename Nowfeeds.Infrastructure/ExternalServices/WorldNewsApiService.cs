using Microsoft.Extensions.Options;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using System.Text.Json;

namespace Nowfeeds.Infrastructure.ExternalServices
{
	public class WorldNewsApiService : IWorldNewsApiService
	{
		private readonly HttpClient _httpClient;
		private readonly WorldNewsApiConfiguration _worldNewsApiConfiguration;

		public WorldNewsApiService(HttpClient httpClient, IOptionsSnapshot<InfrastructureConfiguration> configuration)
		{
			_httpClient = httpClient;
			_worldNewsApiConfiguration = configuration.Value.ExternalServices.WorldNewsApi;
		}

		public async Task<WorldNewsApiResponse> SearchNews(string countryCode, string text, CancellationToken cancellationToken)
		{
			string url = $"{_worldNewsApiConfiguration.BaseUrl}{_worldNewsApiConfiguration.SearchNewsEndpoint}?source-country={countryCode}&text={text}&api-key={_worldNewsApiConfiguration.ApiKey}";
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

			var responseDeserialized = JsonSerializer.Deserialize<WorldNewsApiResponse>(responseContent);

			return responseDeserialized;
		}
	}
}

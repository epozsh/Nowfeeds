using Microsoft.Extensions.Options;
using Nowfeeds.Infrastructure.Config;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using System.Text.Json;

namespace Nowfeeds.Infrastructure.ExternalServices
{
	public class TwitterService : ITwitterService
	{
		private readonly HttpClient _httpClient;
		private readonly TwitterConfiguration _twitterConfiguration;
		public TwitterService(HttpClient httpClient, IOptionsSnapshot<InfrastructureConfiguration> configuration)
		{
			_httpClient = httpClient;
			_twitterConfiguration = configuration.Value.ExternalServices.Twitter;
		}

		public async Task<RecentTweetsApiResponse> SearchRecentTweetsAsync(RecentTweetsRequest recentTweetsReques, CancellationToken cancellationToken)
		{
			string url = $"{_twitterConfiguration.BaseUrl}{_twitterConfiguration.RecentTweetsEndpoint}?query={recentTweetsReques.Query}&start_time={recentTweetsReques.StartTime.ToString("s")}&end_time={recentTweetsReques.EndTime.ToString("s")}&max_results=5&place.fields=full_name,country,geo,id,name,place_type&sort_order=relevancy";

			using var request = new HttpRequestMessage(HttpMethod.Get, url);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _twitterConfiguration.Bearer);
			var response = await _httpClient.SendAsync(request, cancellationToken);

			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

			var responseDeserialized = JsonSerializer.Deserialize<RecentTweetsApiResponse>(responseContent);

			return responseDeserialized;
		}
	}
}

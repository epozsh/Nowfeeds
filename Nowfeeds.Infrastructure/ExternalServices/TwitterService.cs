using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.ExternalServices
{
	public class TwitterService : ITwitterService
	{
		private readonly HttpClient _httpClient;
		public TwitterService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> SearchTweetsAsync(string location, string category)
		{
			return await Task.FromResult("TwitterService: SearchTweetsAsync");
		}
	}
}

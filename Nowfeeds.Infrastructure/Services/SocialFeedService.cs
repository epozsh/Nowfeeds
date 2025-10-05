using Nowfeeds.Application.Interfaces;
using Nowfeeds.Domain.Values;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class SocialFeedService : ISocialFeedService
	{
		private readonly ITwitterService _twitterService;
		private readonly ICacheService _cacheService;

		public SocialFeedService(ITwitterService twitterService, ICacheService cacheService)
		{
			_twitterService = twitterService;
			_cacheService = cacheService;
		}

		public async Task<SocialFeed> GetSocialFeedsAsync(string city, CancellationToken cancellationToken)
		{
			var utcNow = DateTime.UtcNow;


			var twitterFeeds = await _cacheService.GetOrAddAsync(city, () => _twitterService.SearchRecentTweetsAsync(new RecentTweetsRequest()
			{
				Query = city,
				StartTime = utcNow.AddHours(5),
				EndTime = utcNow
			}, cancellationToken), TimeSpan.FromMinutes(15), cancellationToken);

			return new SocialFeed
			{
				Posts = twitterFeeds.Data?.Select(t => t.Text).ToArray() ?? Array.Empty<string>()
			};
		}
	}
}

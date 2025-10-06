using Microsoft.Extensions.Logging;
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
		private readonly ILogger<SocialFeedService> _logger;

		public SocialFeedService(ITwitterService twitterService, ICacheService cacheService, ILogger<SocialFeedService> logger)
		{
			_twitterService = twitterService;
			_cacheService = cacheService;
			_logger = logger;
		}

		public async Task<SocialFeed> GetSocialFeedsAsync(string city, CancellationToken cancellationToken)
		{
			try
			{
				var utcNow = DateTime.UtcNow;

				var twitterFeeds = await _cacheService.GetOrAddAsync(city, () => _twitterService.SearchRecentTweetsAsync(new RecentTweetsRequest()
				{
					Query = city,
					StartTime = utcNow,
					EndTime = utcNow.AddHours(5)
				}, cancellationToken), TimeSpan.FromMinutes(15), cancellationToken);

				return new SocialFeed
				{
					Posts = twitterFeeds.Data?.Select(t => t.Text).ToArray() ?? Array.Empty<string>()
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error fetching social feeds for city: {City}", city);

				return new SocialFeed
				{
					Posts = Array.Empty<string>()
				};
			}
		}
	}
}

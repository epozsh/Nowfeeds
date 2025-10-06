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

				RecentTweetsApiResponse cachedData = await _cacheService.GetAsync<RecentTweetsApiResponse>(city, cancellationToken);

				if (cachedData != null)
				{
					return new SocialFeed
					{
						Posts = cachedData.Data?.Select(t => t.Text).ToArray() ?? Array.Empty<string>()
					};
				}

				RecentTweetsApiResponse response = await _twitterService.SearchRecentTweetsAsync(new RecentTweetsRequest()
				{
					Query = city,
					StartTime = utcNow,
					EndTime = utcNow.AddHours(5)
				}, cancellationToken);

				await _cacheService.AddAsync(city, response, TimeSpan.FromMinutes(15), null, cancellationToken);

				return new SocialFeed
				{
					Posts = response.Data?.Select(t => t.Text).ToArray() ?? Array.Empty<string>()
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

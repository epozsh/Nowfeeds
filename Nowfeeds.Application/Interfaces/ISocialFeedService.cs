using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Interfaces
{
	public interface ISocialFeedService
	{
		Task<SocialFeed> GetSocialFeedsAsync(string city, CancellationToken cancellationToken);
	}
}

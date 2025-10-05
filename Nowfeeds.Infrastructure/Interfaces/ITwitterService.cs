using Nowfeeds.Infrastructure.ExternalServices.Models;

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface ITwitterService
	{
		Task<RecentTweetsApiResponse> SearchRecentTweetsAsync(RecentTweetsRequest recentTweetsReques, CancellationToken cancellationToken);
	}
}

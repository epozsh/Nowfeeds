using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Interfaces
{
	public interface INewsFeedService
	{
		Task<NewsFeed> GetNewsFeedsAsync(string city, CancellationToken cancellationToken);
	}
}

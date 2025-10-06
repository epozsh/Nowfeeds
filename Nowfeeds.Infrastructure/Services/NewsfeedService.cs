using Nowfeeds.Application.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class NewsFeedService : INewsFeedService
	{
		public Task<string> GetNewsFeedsAsync(string location, string category)
		{
			throw new NotImplementedException();
		}
	}
}

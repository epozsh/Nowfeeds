namespace Nowfeeds.Application.Interfaces
{
	public interface INewsFeedService
	{
		Task<string> GetNewsFeedsAsync(string location, string category);
	}
}

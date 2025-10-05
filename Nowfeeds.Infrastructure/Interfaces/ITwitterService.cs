namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface ITwitterService
	{
		Task<string> SearchTweetsAsync(string location, string category);
	}
}

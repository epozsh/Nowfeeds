namespace Nowfeeds.Application.Interfaces
{
	public interface ISocialFeedService
	{
		Task<string> GetSocialFeedsAsync(string location, string category);
	}
}

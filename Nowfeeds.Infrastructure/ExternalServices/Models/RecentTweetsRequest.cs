namespace Nowfeeds.Infrastructure.ExternalServices.Models
{
	public class RecentTweetsRequest
	{
		public string Query { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}

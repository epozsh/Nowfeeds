namespace Nowfeeds.Infrastructure.Metrics
{
	public class Metric
	{
		public string Title { get; set; }
		public int TotalRequests { get; set; }
		public int SuccessCount { get; set; }
		public int FailureCount { get; set; }
		public double TotalResponseTimeMs { get; set; }
		public double AverageResponseTimeMs => TotalRequests == 0 ? 0 : TotalResponseTimeMs / TotalRequests;
		public DateTime LastRequestUtc { get; set; }
	}
}

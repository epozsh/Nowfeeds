namespace Nowfeeds.Application.Interfaces
{
	public interface IMetricsService
	{
		Task<string> GetMetrics(CancellationToken cancellationToken);
	}
}

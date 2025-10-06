using Nowfeeds.Infrastructure.Metrics;

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface IMetricsRecorderService
	{
		Task UpdateMetricsAsync(string key, string title, double responseTimeMs, bool success, CancellationToken cancellationToken);
		Task<Metric[]> GetMetricsAsync(string[] keys, CancellationToken cancellationToken);
	}
}

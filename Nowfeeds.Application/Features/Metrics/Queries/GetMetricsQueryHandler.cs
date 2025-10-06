using MediatR;
using Nowfeeds.Application.Interfaces;

namespace Nowfeeds.Application.Features.Metrics.Queries
{
	public class GetMetricsQueryHandler : IRequestHandler<GetMetricsQuery, string>
	{
		private readonly IMetricsService _metricsService;


		public GetMetricsQueryHandler(IMetricsService metricsService)
		{
			_metricsService = metricsService;
		}

		public async Task<string> Handle(GetMetricsQuery request, CancellationToken cancellationToken)
		{
			return await _metricsService.GetMetrics(cancellationToken);
		}
	}
}

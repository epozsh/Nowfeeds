using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nowfeeds.Application.Features.Metrics.Queries;

namespace Nowfeeds.Api.Controllers
{
	public class MetricsController : ApiBaseController
	{
		public MetricsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
		{
		}

		[HttpGet]
		public async Task<string> GetMetrics(CancellationToken cancellationToken)
		{
			string result = await _mediator.Send(new GetMetricsQuery(), cancellationToken);

			return result;
		}
	}
}

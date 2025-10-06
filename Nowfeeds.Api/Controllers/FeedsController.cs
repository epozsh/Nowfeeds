using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nowfeeds.Api.Models.Feeds;
using Nowfeeds.Application.Features.LocalFeeds.Queries;

namespace Nowfeeds.Api.Controllers
{
	public class FeedsController : ApiBaseController
	{
		public FeedsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
		{
		}

		[HttpGet]
		public async Task<LocalFeedsApiModel> GetLocalFeeds([FromQuery] string city, CancellationToken cancellationToken)
		{
			GetLocalFeedsResult result = await _mediator.Send(new GetLocalFeedsQuery(city), cancellationToken);

			var response = _mapper.Map<LocalFeedsApiModel>(result);

			return response;
		}
	}
}

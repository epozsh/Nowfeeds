using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Nowfeeds.Api.Controllers
{
	[ExcludeFromCodeCoverage]
	[Route("api/[controller]")]
	[ApiController]
	public abstract class ApiBaseController : ControllerBase
	{
		public readonly IMediator _mediator;
		public readonly IMapper _mapper;

		public ApiBaseController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}
	}
}

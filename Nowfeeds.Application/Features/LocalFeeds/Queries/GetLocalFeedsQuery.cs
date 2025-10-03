using MediatR;

namespace Nowfeeds.Application.Features.Weather.Queries
{
	public class GetLocalFeedsQuery : IRequest<string>
	{
		public string Location { get; set; }
		public string Category { get; set; }
	}
}

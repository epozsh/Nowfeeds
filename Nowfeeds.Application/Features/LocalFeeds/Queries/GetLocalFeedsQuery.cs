using MediatR;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQuery : IRequest<GetLocalFeedsResult>
	{
		public GetLocalFeedsQuery(string city)
		{
			City = city;
		}

		public string City { get; }
	}
}

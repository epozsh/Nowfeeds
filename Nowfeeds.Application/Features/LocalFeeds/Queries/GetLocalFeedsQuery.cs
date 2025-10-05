using MediatR;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQuery : IRequest<GetLocalFeedsResult>
	{
		public GetLocalFeedsQuery(string city, string category)
		{
			City = city;
			Category = category;
		}

		public string City { get; }
		public string Category { get; }
	}
}

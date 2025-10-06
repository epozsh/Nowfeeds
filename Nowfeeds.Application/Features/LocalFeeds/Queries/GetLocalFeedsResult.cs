using Nowfeeds.Application.Common;
using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsResult : Result
	{
		public Weather Weather { get; set; }
		public SocialFeed SocialFeed { get; set; }
	}
}

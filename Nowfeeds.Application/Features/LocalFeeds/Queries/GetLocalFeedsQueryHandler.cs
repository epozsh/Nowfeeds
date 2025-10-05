using MediatR;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQueryHandler : IRequestHandler<GetLocalFeedsQuery, GetLocalFeedsResult>
	{
		private readonly IWeatherService _weatherService;
		private readonly INewsFeedService _newsFeedService;
		private readonly ISocialFeedService _socialFeedService;

		public GetLocalFeedsQueryHandler(IWeatherService weatherService, INewsFeedService newsFeedService, ISocialFeedService socialFeedService)
		{
			_weatherService = weatherService;
			_newsFeedService = newsFeedService;
			_socialFeedService = socialFeedService;
		}

		public async Task<GetLocalFeedsResult> Handle(GetLocalFeedsQuery request, CancellationToken cancellationToken)
		{
			Weather weather = await _weatherService.GetWeatherAsync(request.City, cancellationToken);
			SocialFeed social = await _socialFeedService.GetSocialFeedsAsync(request.City, cancellationToken);

			return new GetLocalFeedsResult()
			{
				Weather = weather,
				SocialFeed = social,
			};
		}
	}
}

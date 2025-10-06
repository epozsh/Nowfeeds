using MediatR;
using Microsoft.Extensions.Logging;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Domain.Values;

namespace Nowfeeds.Application.Features.LocalFeeds.Queries
{
	public class GetLocalFeedsQueryHandler : IRequestHandler<GetLocalFeedsQuery, GetLocalFeedsResult>
	{
		private readonly IWeatherService _weatherService;
		private readonly ISocialFeedService _socialFeedService;
		private readonly ILogger<GetLocalFeedsQueryHandler> _logger;

		public GetLocalFeedsQueryHandler(IWeatherService weatherService, ISocialFeedService socialFeedService, ILogger<GetLocalFeedsQueryHandler> logger)
		{
			_weatherService = weatherService;
			_socialFeedService = socialFeedService;
			_logger = logger;
		}

		public async Task<GetLocalFeedsResult> Handle(GetLocalFeedsQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Handling GetLocalFeedsQuery for city: {City}", request.City);

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

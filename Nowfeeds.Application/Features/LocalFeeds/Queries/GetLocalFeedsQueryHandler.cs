using MediatR;
using Nowfeeds.Application.Interfaces;

namespace Nowfeeds.Application.Features.Weather.Queries
{
	public class GetLocalFeedsQueryHandler : IRequestHandler<GetLocalFeedsQuery, string>
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

		public async Task<string> Handle(GetLocalFeedsQuery request, CancellationToken cancellationToken)
		{
			return string.Empty;
		}
	}
}

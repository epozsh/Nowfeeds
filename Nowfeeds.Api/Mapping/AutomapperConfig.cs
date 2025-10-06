using AutoMapper;
using Nowfeeds.Api.Models;
using Nowfeeds.Api.Models.Feeds;
using Nowfeeds.Application.Common;
using Nowfeeds.Application.Common.Extensions;
using Nowfeeds.Application.Features.LocalFeeds.Queries;
using Nowfeeds.Domain.Values;

namespace Nowfeeds.Api.Mapping
{
	public class AutomapperConfig : Profile
	{
		public AutomapperConfig()
		{
			CreateMap<Result, ApiResponseModel>()
				.ForMember(dest => dest.Success, opt => opt.MapFrom(src => !src.ErrorCodeStatus.HasValue))
				.ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.ErrorCodeStatus.HasValue ? src.ErrorCodeStatus.Value.GetDescription() : null));

			CreateMap<GetLocalFeedsResult, LocalFeedsApiModel>()
				.ForMember(dest => dest.Success, opt => opt.MapFrom(src => !src.ErrorCodeStatus.HasValue))
				.ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.ErrorCodeStatus.HasValue ? src.ErrorCodeStatus.Value.GetDescription() : null));

			CreateMap<Weather, WeatherApiModel>();
			CreateMap<SocialFeed, SocialFeedApiModel>();
		}
	}
}

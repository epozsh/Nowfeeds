namespace Nowfeeds.Api.Models.Feeds
{
	public class LocalFeedsApiModel : ApiResponseModel
	{
		public WeatherApiModel Weather { get; set; }
		public SocialFeedApiModel Social { get; set; }
		public NewsFeedApiModel News { get; set; }
	}

	public class WeatherApiModel
	{
		public string Description { get; set; }
		public double Temperature { get; set; }
		public int Humidity { get; set; }
		public double WindSpeed { get; set; }
	}

	public class SocialFeedApiModel
	{
		public string[] Posts { get; set; }
	}

	public class NewsFeedApiModel
	{
		public string[] Articles { get; set; }
	}
}

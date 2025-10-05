namespace Nowfeeds.Api.Models.Feeds
{
	public class LocalFeedsApiModel : ApiResponseModel
	{
		public WeatherApiModel Weather { get; set; }
	}

	public class WeatherApiModel
	{
		public string Description { get; set; }
		public double Temperature { get; set; }
		public int Humidity { get; set; }
		public double WindSpeed { get; set; }
	}
}

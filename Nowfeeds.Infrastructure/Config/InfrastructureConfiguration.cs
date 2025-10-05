namespace Nowfeeds.Infrastructure.Config
{
	public class InfrastructureConfiguration
	{
		public ExternalServices ExternalServices { get; set; }
	}

	public class ExternalServices
	{
		public OpenWeatherMapConfiguration OpenWeatherMap { get; set; }
	}
}

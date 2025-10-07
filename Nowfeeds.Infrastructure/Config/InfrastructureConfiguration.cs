namespace Nowfeeds.Infrastructure.Config
{
	public class InfrastructureConfiguration
	{
		public ExternalServices ExternalServices { get; set; }
		public CacheSettings CacheSettings { get; set; }
	}

	public class ExternalServices
	{
		public OpenWeatherMapConfiguration OpenWeatherMap { get; set; }
		public TwitterConfiguration Twitter { get; set; }
		public WorldNewsApiConfiguration WorldNewsApi { get; set; }
	}
}

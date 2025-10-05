using Autofac;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Infrastructure.ExternalServices;
using Nowfeeds.Infrastructure.Interfaces;
using Nowfeeds.Infrastructure.Services;

namespace Nowfeeds.Infrastructure
{
	public class InfrastructureModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c =>
			{
				var httpClientFactory = c.Resolve<IHttpClientFactory>();
				return httpClientFactory.CreateClient();
			}).As<HttpClient>();


			// External Services
			builder.RegisterType<OpenWeatherMap>().As<IOpenWeatherMap>().InstancePerLifetimeScope();


			// Application Services
			builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
			builder.RegisterType<SocialFeedService>().As<ISocialFeedService>().InstancePerLifetimeScope();
			builder.RegisterType<NewsFeedService>().As<INewsFeedService>().InstancePerLifetimeScope();
		}
	}
}

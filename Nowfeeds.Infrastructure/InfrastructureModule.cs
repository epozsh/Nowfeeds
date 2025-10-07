using Autofac;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Infrastructure.Cache;
using Nowfeeds.Infrastructure.Decorators.ExternalServices;
using Nowfeeds.Infrastructure.ExternalServices;
using Nowfeeds.Infrastructure.Interfaces;
using Nowfeeds.Infrastructure.Metrics;
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
			builder.RegisterType<OpenWeatherMapService>().As<IOpenWeatherMapService>().SingleInstance();
			builder.RegisterType<TwitterService>().As<ITwitterService>().SingleInstance();
			builder.RegisterType<WorldNewsApiService>().As<IWorldNewsApiService>().SingleInstance();

			// Caching
			builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();

			// Metrics
			builder.RegisterType<MetricsRecorderService>().As<IMetricsRecorderService>().SingleInstance();

			// Application Services
			builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
			builder.RegisterType<SocialFeedService>().As<ISocialFeedService>().InstancePerLifetimeScope();
			builder.RegisterType<NewsFeedService>().As<INewsFeedService>().InstancePerLifetimeScope();
			builder.RegisterType<MetricsService>().As<IMetricsService>().InstancePerLifetimeScope();

			// Decorators
			builder.RegisterDecorator(typeof(OpenWeatherMapServiceMetricsDecorator), typeof(IOpenWeatherMapService));
			builder.RegisterDecorator(typeof(TwitterServiceMetricsDecorator), typeof(ITwitterService));
			builder.RegisterDecorator(typeof(WorldNewsApiServiceMetricsDecorator), typeof(IWorldNewsApiService));
		}
	}
}

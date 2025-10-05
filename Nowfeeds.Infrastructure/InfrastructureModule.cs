using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Infrastructure.Cache;
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
			builder.RegisterType<OpenWeatherMapService>().As<IOpenWeatherMapService>().InstancePerLifetimeScope();
			builder.RegisterType<TwitterService>().As<ITwitterService>().InstancePerLifetimeScope();

			// Caching
			builder.RegisterType<InMemoryCacheService>().As<ICacheService>().SingleInstance();
			builder.Register(context => new MemoryCache(new MemoryCacheOptions())).As<IMemoryCache>().SingleInstance();

			// Application Services
			builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
			builder.RegisterType<SocialFeedService>().As<ISocialFeedService>().InstancePerLifetimeScope();
			builder.RegisterType<NewsFeedService>().As<INewsFeedService>().InstancePerLifetimeScope();
		}
	}
}

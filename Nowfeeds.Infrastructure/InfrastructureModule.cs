using Autofac;
using Nowfeeds.Application.Interfaces;
using Nowfeeds.Infrastructure.Services;

namespace Nowfeeds.Infrastructure
{
	public class InfrastructureModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
		}
	}
}

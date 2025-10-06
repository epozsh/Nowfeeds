using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using System.Reflection;

namespace Nowfeeds.Api
{
	public class ApiModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			builder.RegisterAutoMapper(false, assemblies);
		}
	}
}

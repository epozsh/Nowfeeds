using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Nowfeeds.Application.Decorators;
using Nowfeeds.Application.Features.LocalFeeds.Queries;

namespace Nowfeeds.Application
{
	public class ApplicationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var configuration = MediatRConfigurationBuilder
			   .Create("testlicence", typeof(GetLocalFeedsQuery).Assembly)
			   .WithAllOpenGenericHandlerTypesRegistered()
			   .Build();

			// this will add all your Request- and Notificationhandler
			// that are located in the same project as your program-class
			builder.RegisterMediatR(configuration);

			builder.RegisterGenericDecorator(typeof(ValidationDecorator<,>), typeof(IRequestHandler<,>));
		}
	}
}

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Nowfeeds.Api;
using Nowfeeds.Api.Middlewares;
using Nowfeeds.Application;
using Nowfeeds.Infrastructure;
using Nowfeeds.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<InfrastructureConfiguration>(builder.Configuration.GetSection("InfrastructureConfiguration"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	containerBuilder.RegisterModule(new ApiModule());
	containerBuilder.RegisterModule(new ApplicationModule());
	containerBuilder.RegisterModule(new InfrastructureModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

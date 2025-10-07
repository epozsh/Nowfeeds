using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Nowfeeds.Api;
using Nowfeeds.Api.Config;
using Nowfeeds.Api.Middlewares;
using Nowfeeds.Api.Services;
using Nowfeeds.Application;
using Nowfeeds.Infrastructure;
using Nowfeeds.Infrastructure.Config;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ApiConfiguration"));
builder.Services.Configure<InfrastructureConfiguration>(builder.Configuration.GetSection("InfrastructureConfiguration"));

var apiConfig = builder.Configuration.GetSection("ApiConfiguration").Get<ApiConfiguration>();
var jwtConfig = apiConfig.Jwt;

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtConfig.Issuer,
		ValidAudience = jwtConfig.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
	};
});

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<JwtTokenService>();

builder.Host.UseSerilog((ctx, lc) => lc
	.ReadFrom.Configuration(ctx.Configuration));

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

app.MapHealthChecks("/health");

app.UseMiddleware<ExceptionsMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();

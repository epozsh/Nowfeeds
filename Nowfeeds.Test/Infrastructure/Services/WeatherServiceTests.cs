using Moq;
using Nowfeeds.Domain.Values;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using Nowfeeds.Infrastructure.Services;

namespace Nowfeeds.Test.Infrastructure.Services
{

	public class WeatherServiceTests
	{
		[Fact]
		public async Task GetWeatherAsync_ReturnsWeather()
		{
			// Arrange
			var mockWeatherApi = new Mock<IOpenWeatherMapService>();
			var expectedWeather = new Weather { Description = "Clear", Temperature = 20.0 };
			mockWeatherApi.Setup(x => x.GetCurrentWeather(It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new OpenWeatherMapCurrentApiResponse()
				{
					Weather = new List<WeatherData>()
					{
						new WeatherData { Description = "Clear" }
					},
					Main = new Main
					{
						Temp = 20.0,
						Humidity = 50
					},
					Wind = new Wind
					{
						Speed = 5.0
					},
				});

			var service = new WeatherService(mockWeatherApi.Object);

			// Act
			var result = await service.GetWeatherAsync("Thessaloniki", CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Clear", result.Description);
			Assert.Equal(20.0, result.Temperature);
			Assert.Equal(50.0, result.Humidity);
			Assert.Equal(5.0, result.WindSpeed);
		}
	}
}

using Microsoft.Extensions.Logging;
using Moq;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;
using Nowfeeds.Infrastructure.Services;

namespace Nowfeeds.Test.Infrastructure.Services
{
	public class SocialFeedServiceTests
	{
		[Fact]
		public async Task GetSocialFeedsAsync_ReturnsPosts()
		{
			// Arrange
			var mockTwitterService = new Mock<ITwitterService>();
			var mockCacheService = new Mock<ICacheService>();
			var mockLogger = new Mock<ILogger<SocialFeedService>>();

			var tweetsResponse = new RecentTweetsApiResponse
			{
				Data = new System.Collections.Generic.List<Datum>
			{
				new Datum { Text = "Hello World" },
				new Datum { Text = "Test Tweet" }
			}
			};

			mockCacheService.Setup(x => x.GetAsync<RecentTweetsApiResponse>(
				It.IsAny<string>(),
				It.IsAny<CancellationToken>()))
				.ReturnsAsync(tweetsResponse);

			var service = new SocialFeedService(mockTwitterService.Object, mockCacheService.Object, mockLogger.Object);

			// Act
			var result = await service.GetSocialFeedsAsync("Thessaloniki", CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Posts.Length);
			Assert.Contains("Hello World", result.Posts);
			Assert.Contains("Test Tweet", result.Posts);
		}
	}
}

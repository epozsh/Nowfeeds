using Nowfeeds.Application.Interfaces;
using Nowfeeds.Domain.Values;
using Nowfeeds.Infrastructure.ExternalServices.Models;
using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Services
{
	public class NewsFeedService : INewsFeedService
	{
		private readonly IWorldNewsApiService _worldNewsApiService;

		public NewsFeedService(IWorldNewsApiService worldNewsApiService)
		{
			_worldNewsApiService = worldNewsApiService;
		}

		public async Task<NewsFeed> GetNewsFeedsAsync(string city, CancellationToken cancellationToken)
		{
			string countryCode = "gr"; // Test purpoces

			WorldNewsApiResponse data = await _worldNewsApiService.SearchNews(countryCode, city, cancellationToken);

			return new NewsFeed()
			{
				Articles = data?.News?.Select(n => new Article
				{
					Title = n.Title,
					Summary = n.Summary
				}).ToArray() ?? Array.Empty<Article>()
			};
		}
	}
}

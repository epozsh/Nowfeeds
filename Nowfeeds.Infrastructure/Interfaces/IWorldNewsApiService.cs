using Nowfeeds.Infrastructure.ExternalServices.Models;

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface IWorldNewsApiService
	{
		Task<WorldNewsApiResponse> SearchNews(string country, string text, CancellationToken cancellationToken);
	}
}

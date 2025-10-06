namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface ICacheService
	{
		Task<T> GetAsync<T>(string key, CancellationToken cancellationToken);
		Task AddAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidaingExpiration = null, CancellationToken cancellationToken = default);
		Task RemoveAsync(string key, CancellationToken cancellationToken);
	}
}

namespace Nowfeeds.Infrastructure.Interfaces
{
	public interface ICacheService
	{
		Task<T?> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default);
		void Remove(string key);
	}
}

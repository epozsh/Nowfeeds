using Microsoft.Extensions.Caching.Memory;
using Nowfeeds.Infrastructure.Interfaces;

namespace Nowfeeds.Infrastructure.Cache
{
	public class InMemoryCacheService : ICacheService
	{
		private readonly IMemoryCache _memoryCache;

		public InMemoryCacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public async Task<T?> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
		{
			if (_memoryCache.TryGetValue(key, out T? value))
				return value;

			value = await valueFactory();
			var options = new MemoryCacheEntryOptions();
			if (absoluteExpiration.HasValue)
				options.SetAbsoluteExpiration(absoluteExpiration.Value);

			_memoryCache.Set(key, value, options);
			return value;
		}

		public void Remove(string key)
		{
			_memoryCache.Remove(key);
		}
	}
}

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nowfeeds.Infrastructure.Interfaces;
using System.Text.Json;

namespace Nowfeeds.Infrastructure.Cache
{
	public class CacheService : ICacheService
	{
		private readonly IDistributedCache _distributedCache;
		private readonly ILogger<CacheService> _logger;

		public CacheService(IDistributedCache distributedCache, ILogger<CacheService> logger)
		{
			_distributedCache = distributedCache;
			_logger = logger;
		}

		public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
		{
			try
			{
				var cached = await _distributedCache.GetStringAsync(key, cancellationToken);
				if (cached == null)
					return default;

				return JsonSerializer.Deserialize<T>(cached);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving cache for key: {Key}", key);
				return default;
			}
		}

		public async Task AddAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidaingExpiration = null, CancellationToken cancellationToken = default)
		{
			try
			{
				var options = new DistributedCacheEntryOptions();
				if (absoluteExpiration.HasValue)
					options.SetAbsoluteExpiration(absoluteExpiration.Value);
				if (slidaingExpiration.HasValue)
					options.SetSlidingExpiration(slidaingExpiration.Value);

				var serialized = JsonSerializer.Serialize(value);
				await _distributedCache.SetStringAsync(key, serialized, options, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding cache for key: {Key}", key);
			}
		}

		public async Task RemoveAsync(string key, CancellationToken cancellationToken)
		{
			try
			{
				await _distributedCache.RemoveAsync(key, cancellationToken);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing cache for key: {Key}", key);
			}
		}
	}
}

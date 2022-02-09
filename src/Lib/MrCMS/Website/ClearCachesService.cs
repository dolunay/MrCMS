using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using MrCMS.Services.Caching;
using MrCMS.Website.Caching;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Impl;

namespace MrCMS.Website
{
    public class ClearCachesService : IClearCachesService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IEnumerable<IClearCache> _manualCacheClears;
        private readonly ISessionFactory _factory;
        private readonly IHighPriorityCacheManager _highPriorityCacheManager;
        private readonly IMemoryCache _memoryCache;

        public ClearCachesService(ICacheManager cacheManager, IEnumerable<IClearCache> manualCacheClears, ISessionFactory factory,
            IHighPriorityCacheManager highPriorityCacheManager, IMemoryCache memoryCache)
        {
            _cacheManager = cacheManager;
            _manualCacheClears = manualCacheClears;
            _factory = factory;
            _highPriorityCacheManager = highPriorityCacheManager;
            _memoryCache = memoryCache;
        }

        public void ClearCache()
        {
            _cacheManager.Clear();

            foreach (var cache in _manualCacheClears)
            {
                cache.ClearCache();
            }

            foreach (var (_, value) in (_factory as SessionFactoryImpl)?.GetAllSecondLevelCacheRegions()?? new Dictionary<string, ICache>())
            {
                value.Clear();
            }
        }

        public void ClearHighPriorityCache()
        {
            _highPriorityCacheManager.Clear();
        }
    }
}
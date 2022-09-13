using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Wallet.Service.Util
{
    public class CacheService 
    {
        private IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 寫入快取資料
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="ExpiraMinutes">過期時間 分計算</param>
        /// <returns></returns>
        public bool SetCache(string key, object obj, int ExpiraMinutes = 0)
        {
            try
            {
                if (ExpiraMinutes == 0)
                {
                    _memoryCache.Set(key, obj);
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(ExpiraMinutes));
                    _memoryCache.Set(key, obj, cacheEntryOptions);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public dynamic GetCache(string key)
        {
            return _memoryCache.Get(key);
        }

        /// <summary>
        /// 移除資料
        /// </summary>
        /// <param name="key"></param>
        public bool RemoveCache(string key)
        {
            try
            {
                _memoryCache.Remove(key);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

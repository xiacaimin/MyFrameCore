using CSRedis;
using MyFrameCore.Model;
using System;

namespace MyFrameCore.Api.Common
{
    public class RedisHelper
    {
        public RedisHelper(AppSettings appsettings)
        {
            if (QuickHelperBase.Instance == null)
            {
                QuickHelperBase.Instance = new ConnectionPool(appsettings.Redis.IP, Convert.ToInt32(appsettings.Redis.Port), Convert.ToInt32(appsettings.Redis.PoolSize));
            }
        }
        public string Set(string key, string value, int expireSeconds = -1)
        {
            return QuickHelperBase.Set(key, value, expireSeconds);
        }
        public string Get(string key)
        {
            return QuickHelperBase.Get(key);
        }
        public long Remove(params string[] key)
        {
            return QuickHelperBase.Remove(key);
        }
        public bool Exists(string key)
        {
            return QuickHelperBase.Exists(key);
        }
        public long Increment(string key, long value = 1)
        {
            return QuickHelperBase.Increment(key, value);
        }
        public bool Expire(string key, TimeSpan expire)
        {
            return QuickHelperBase.Expire(key, expire);
        }
    }
}

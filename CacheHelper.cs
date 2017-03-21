using System;
using System.Runtime.Caching;

namespace ExpressBase.Studio
{
    internal class CacheHelper
    {
        private static ObjectCache cache = MemoryCache.Default;

        internal static T Get<T>(string key)
        {
            T result = default(T);

            object value = cache.Get(key);
            if (value != null)
            {
                if (value is T)
                    result = (T)value;
                else
                    throw new InvalidCastException(string.Format("Unable to cast {0} to {1}.", value.GetType().Name, typeof(T).Name));
            }

            return result;
        }

        internal static void Set(string key, object value)
        {
            cache.Set(key, value, null);
        }

        internal static void Remove(string key)
        {
            if (cache.Contains(key))
                cache.Remove(key);
        }

        internal static bool Contains(string key)
        {
            return cache.Contains(key);
        }

        internal static string SERVICESTACK_URL
        {
            get { return Get<string>(CacheKeys.SERVICESTACK_URL); }
        }
    }

    internal class CacheKeys
    {
        internal const string SERVICESTACK_URL = "ServiceStackUrl";
    }
}
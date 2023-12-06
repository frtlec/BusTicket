using BusTicket.Adapter.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business.Caching
{
    public interface IBusTicketCache
    {

        /// <summary>
        /// SECOND
        /// </summary>
        public const int BUS_LOCATION_EXPIRE = 600;
        public const string BUS_LOCATION_KEY = "BUS_LOCATIONS";


        (bool, T) Get<T>(string key);
        void Set<T>(string key, T data, int expire = 120);
    }
    public class BusTicketCache : IBusTicketCache
    {
        private readonly IMemoryCache _memoryCache;

        public BusTicketCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public (bool, T) Get<T>(string key)
        {
            bool hasLocations = _memoryCache.TryGetValue(key, out T data);
            return (hasLocations, data);
        }

        public void Set<T>(string key, T data, int expire = 120)
        {
            _memoryCache.Set(key, data, DateTime.Now.AddSeconds(expire));
        }
    }
}

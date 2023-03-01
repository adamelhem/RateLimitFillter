using Microsoft.Extensions.Caching.Memory;
using RateLimitFillter;
using RateLimitFillter.Controllers;

namespace RateLimitProtection.Protections
{
    public class IPsAntiSpam : IIPsAntiSpam
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<IPsAntiSpam> _logger;
        private RateLimitMemRec _lastIpRecord;

        public IPsAntiSpam(ILogger<IPsAntiSpam> logger)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions() { });
            _logger = logger;
        }

        public bool isIpRequestReachedLimitCount(string clientIp)
        {
            var cacheRec = _memoryCache.Get(clientIp);

            if (cacheRec == null)
            {
                try
                {
                    _lastIpRecord = new RateLimitMemRec
                    {
                        time = DateTime.Now,
                        count = 0
                    };
                    _memoryCache.Set(clientIp, _lastIpRecord, DateTime.Now.AddSeconds(20));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Message:{ex.Message},StackTrace:{ex.StackTrace},");
                }
            }
            else
            {
                _lastIpRecord = cacheRec as RateLimitMemRec;
                _lastIpRecord.count++;
                _memoryCache.Set(clientIp, _lastIpRecord, DateTime.Now.AddSeconds(20));
            }
            if(_lastIpRecord.count > 10)
            {
                return true;
            }
            return false;
        }


    }
}

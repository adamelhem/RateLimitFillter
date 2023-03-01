namespace RateLimitProtection.Common
{
    public class Utils : IUtils
    {
        public string GetClientIpAddress(HttpRequest request)
        {
            return request.Host.Value;
        }
    }
}
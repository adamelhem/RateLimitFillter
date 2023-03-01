namespace RateLimitProtection.Common
{
    public interface IUtils
    {
        string GetClientIpAddress(HttpRequest request);
    }
}
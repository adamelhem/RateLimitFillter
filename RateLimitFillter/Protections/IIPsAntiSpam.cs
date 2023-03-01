namespace RateLimitProtection.Protections
{
    public interface IIPsAntiSpam
    {
        bool isIpRequestReachedLimitCount(string clientIp);
    }
}
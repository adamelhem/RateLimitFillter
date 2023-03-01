using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using RateLimitFillter;
using RateLimitFillter.Controllers;
using RateLimitProtection.Common;
using RateLimitProtection.Protections;
using System.Net;

namespace RateLimitProtection.Fillters
{
    public class RequestRateLimitFillter : ActionFilterAttribute, IAsyncActionFilter
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly IIPsAntiSpam _IPsAntiSpam;
        private readonly IUtils _IUtils;

        public RequestRateLimitFillter(ILogger<HelloWorldController> logger , IIPsAntiSpam IPsAntiSpam, IUtils utils)
        {
            _logger = logger;
            _IPsAntiSpam = IPsAntiSpam;
            _IUtils = utils;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(await RateLimitCheckReached(context))
            {
                context.Result = new UnauthorizedResult(); 
                return;
            }
            else
            {
                next();
            }
        }

        private async Task<bool> RateLimitCheckReached(ActionExecutingContext context)
        {
            var ip = _IUtils.GetClientIpAddress(context.HttpContext.Request);
            return _IPsAntiSpam.isIpRequestReachedLimitCount(ip);
        }

    }
}

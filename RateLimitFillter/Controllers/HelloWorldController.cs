using Microsoft.AspNetCore.Mvc;

namespace RateLimitFillter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHelloWorld")]
        public async Task<IActionResult> Get()
        {
            return Ok("hello world");
        }
    }
}
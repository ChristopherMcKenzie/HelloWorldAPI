using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorldAPI.Controllers
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
        
        [HttpGet]
        public async Task<String> Get(string name)
        {
            Request.Headers.TryGetValue("User-Agent", out var header);          
            await Task.Delay(100);
            return name + " " + header;
        }
    }
}
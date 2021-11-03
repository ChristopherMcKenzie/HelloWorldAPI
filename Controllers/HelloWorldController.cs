using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
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
            Task<String> result = null;

            using (var cts = new CancellationTokenSource())
            {
                var delayTask = Task.Delay(1, cts.Token);
                
                var agentResult = Request.Headers.TryGetValue("User-Agent", out var header);
                var resultString = agentResult ? name + " " + header : "No agent found";
                
                result = Task.FromResult(resultString);

                var resultTask = await Task.WhenAny(result, delayTask);
                
                if (resultTask == delayTask)
                {
                    throw new OperationCanceledException();
                }
                else
                {
                    cts.Cancel();
                }
                return await result;
            }
        }
    }
}
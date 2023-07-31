using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AppInsightsLogging
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string msg = req.Query["msg"];
            string type = req.Query["type"];
            string logTime = req.Query["logtime"];
            string delayTime = req.Query["dt"];
            int delayTimeSecond = int.Parse(delayTime) * 1000;
            if (type == "Error")
            {
                log.LogError($"Logged Time {logTime} Message {msg}");
            }
            else if (type == "warning")
            {
                log.LogWarning($"Logged Time {logTime} Message {msg}");
            }
            else if (type == "information")
            {
                log.LogInformation($"Logged Time {logTime} Message {msg}");
            }
            else if (type == "performance")
            {

                log.LogWarning("chance of perfomance issue here");

                await Task.Delay(delayTimeSecond);
            }


            return new OkObjectResult(msg);
        }
    }
}

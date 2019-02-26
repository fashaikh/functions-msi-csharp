using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace msiarm
{
    public static class GetResourceGroups
    {
        [FunctionName("GetResourceGroups")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<ARMRequest>(requestBody);

            var afc = await ArmFluentClient.GetAzureClient(log);

            var resourcegroups = await afc.WithDefaultSubscription().ResourceGroups.ListAsync();

            return (ActionResult)new OkObjectResult( new ARMRequest { Body = resourcegroups });

        }
    }
    public class ARMRequest {
        public string Verb { get; set; }
        public string ResourceId { get; set; }
        public string RawHeaders { get; set; }
        public object Body { get; set; }

    }
}

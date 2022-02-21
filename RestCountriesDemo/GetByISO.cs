
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;

namespace RestCountriesDemo
{
    public static class GetByISO
    {
        [FunctionName("GetByISO")]
        
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "countries/{code}")]HttpRequest req, string code, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var client = new HttpClient();

            var response = await client.GetAsync(Environment.GetEnvironmentVariable("COUNTRIES_API_URL_GetByISO")+code);

            var content = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(content);

            var output = JsonConvert.DeserializeObject<dynamic>("{ }");

            return output as JObject != null
                ? (ActionResult)new OkObjectResult(data)
                : new BadRequestObjectResult($"The country with ISO 3166 Alpha 3 code '{code}'could not be found.");
        }
    }
}

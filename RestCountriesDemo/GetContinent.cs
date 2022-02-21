
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
    public static class GetContinent
    {
        [FunctionName("GetContinent")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "continents/{region}/countries")]HttpRequest req, string region, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");


            var client = new HttpClient();

            var response = await client.GetAsync(Environment.GetEnvironmentVariable("COUNTRIES_API_URL_GetContinent")+region);

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject(content);

            var output = JsonConvert.DeserializeObject<dynamic>("{ }");


            if (output as JObject != null)
            {
                return new OkObjectResult(data);
            }
            else
            {
                return new BadRequestObjectResult($"The continent with name '{region}' could not be found.");
            }

        }
       
    }
}

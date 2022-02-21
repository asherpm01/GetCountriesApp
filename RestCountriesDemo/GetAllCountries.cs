
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace RestCountriesDemo
{
    public static class Function1
    {
        [FunctionName("GetAllCountries")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var client = new HttpClient();

            var response = await client.GetAsync(Environment.GetEnvironmentVariable("COUNTRIES_API_URL_GetAllCountries"));

            var content = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(content);

            var output = JsonConvert.DeserializeObject<dynamic>("{ }");

            if (data != null)
            {
                return new OkObjectResult(data);
            }
            else
            {
                return new BadRequestObjectResult("The list of countries could not be found");
            }
        }
    }
}

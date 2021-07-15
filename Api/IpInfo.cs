using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

using BlazorApp.Shared;

using IpInfo;

namespace BlazorApp.Api
{
    public static class IpInfo
    {
        [FunctionName("GetGeoLoc")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var token = Environment.GetEnvironmentVariable("IPINFO_TOKEN");
            return new OkObjectResult(token);
        }

        [FunctionName("LogGeoLoc")]
        public async static Task<IActionResult> LogGeoLoc(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = null)] HttpRequest req,
            ILogger log) 
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
                    
            var responseInfo = JsonConvert.DeserializeObject<FullResponse>(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var geoInfo = new GeoInfo(responseInfo);

            var db = new CosmosDBOps();
            await db.StoreToCosmosDB<GeoInfo>(geoInfo);

            return new OkResult();
        }
    }
}
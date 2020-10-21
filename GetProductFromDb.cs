using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FunctionApp1
{
    public static class GetProductFromDb
    {
        [FunctionName("GetProductFromDb")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] 
            HttpRequest req,

            [CosmosDB(databaseName: "ProductDB", collectionName: "ProductCollection", 
                SqlQuery = "SELECT top 100 * FROM product order by product._ts desc", 
                ConnectionStringSetting = "r3-access_DOCUMENTDB")]
            IEnumerable<Product> products,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string data = JsonConvert.SerializeObject(products);

            string responseMessage = "All ok";

            return new OkObjectResult(data);
        }
    }
}

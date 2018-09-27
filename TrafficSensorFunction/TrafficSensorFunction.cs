using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TrafficSensorFunctionApp.Models;

namespace TrafficSensorFunctionApp
{
    public static class TrafficSensorFunctionApp
    {
        [FunctionName("TrafficSensorFunctionApp")]
        public static async Task Run([EventHubTrigger("trafficeventhub", Connection = "EventHubConnection", ConsumerGroup = "$Default")]string[] eventhubMessages, [DocumentDB(
                databaseName: Constants.DatabaseName,
                collectionName: Constants.CollName,
                ConnectionStringSetting = "CosmosConnection")] IAsyncCollector<TrafficMessage> trafficMessages, TraceWriter log)
        {
            log.Info($"C# Event Hub trigger function processed a message");
            HttpClient httpClient = new HttpClient();
            PowerBiRequestPayload document = new PowerBiRequestPayload();

            foreach (var ehMessage in eventhubMessages)
            {
                var trafficMessage = JsonConvert.DeserializeObject<TrafficMessage>(ehMessage);
                document.rows.Add(trafficMessage);
                await trafficMessages.AddAsync(trafficMessage);
            }
            var myContent = JsonConvert.SerializeObject(document);
            var content = new StringContent(myContent, Encoding.UTF8, "application/json");
            var powerBI = Environment.GetEnvironmentVariable("PowerBIEndpoint");
            var result = await httpClient.PostAsync(powerBI, content);
        }
    }
}

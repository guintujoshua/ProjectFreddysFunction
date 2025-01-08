using Azure.Messaging.ServiceBus;
using FreddysFunctions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FreddysFunctions.HttpTrigger
{
    public class UpsertScore
    {
        private readonly ILogger<UpsertScore> _logger;
        private readonly string _serviceBusConnectionString;
        private readonly string _queueName;

        public UpsertScore(ILogger<UpsertScore> logger, IConfiguration configuration)
        {
            _logger = logger;
            _serviceBusConnectionString = configuration.GetConnectionString("sbq-upsert-score") ?? throw new ArgumentNullException(nameof(configuration), "Service Bus connection string cannot be null");
            _queueName = "sbq-upsert-score";
        }

        [Function("UpsertScore")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string country = req.Query["Country"].ToString();
            string score = req.Query["Score"].ToString();

            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(score) || !int.TryParse(score, out int parsedScore))
            {
                return new BadRequestObjectResult("Invalid input");
            }

            // Send message to Service Bus with random SessionId
            Random random = new Random();
            string sessionId = random.Next(0, 99999999).ToString();
            string message = JsonConvert.SerializeObject(new { Country = country, Score = parsedScore });
            await SendMessageToServiceBusAsync(_serviceBusConnectionString, _queueName, message, sessionId);

            return new OkObjectResult("Success");
        }

        private static async Task SendMessageToServiceBusAsync(string connectionString, string queueName, string message, string sessionId)
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                ServiceBusSender sender = client.CreateSender(queueName);
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message)
                {
                    SessionId = sessionId
                };

                // Send the message
                await sender.SendMessageAsync(serviceBusMessage);
            }
        }
    }
}

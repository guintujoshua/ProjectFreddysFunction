using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using FreddysFunctions.Model;
using FreddysFunctions.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FreddysFunctions.QueueTrigger
{
    public class UpsertScoreQueue
    {
        private readonly ILogger<UpsertScoreQueue> _logger;
        private readonly ILeaderboardService _leaderboardService;

        public UpsertScoreQueue(ILogger<UpsertScoreQueue> logger, ILeaderboardService leaderboardService)
        {
            _logger = logger;
            _leaderboardService = leaderboardService;
        }

        [Function(nameof(UpsertScoreQueue))]
        public async Task Run(
            [ServiceBusTrigger("sbq-upsert-score", Connection = "sbq-upsert-score", IsSessionsEnabled = true)]
                ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            UpsertScoreModel request = message.Body.ToObjectFromJson<UpsertScoreModel>();
            await  _leaderboardService.UpsertScore(request.Country, request.Score);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}

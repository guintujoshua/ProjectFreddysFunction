using FreddysFunctions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FreddysFunctions.HttpTrigger
{
    public class GetLeaderboard
    {
        private readonly ILogger<GetLeaderboard> _logger;
        private readonly ILeaderboardService _leaderboardService;
        public GetLeaderboard(ILogger<GetLeaderboard> logger, ILeaderboardService leaderboardService)
        {
            _logger = logger;
            _leaderboardService = leaderboardService;
        }

        [Function("GetLeaderboard")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Call the service
            return new OkObjectResult(await _leaderboardService.GetLeaderboard());
        }
    }
}

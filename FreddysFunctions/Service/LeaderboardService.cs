using FreddysFunctions.Model;
using FreddysFunctions.Provider;
namespace FreddysFunctions.Service
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardProvider _leaderboardProvider;
        public LeaderboardService(ILeaderboardProvider leaderboardProvider)
        {
            _leaderboardProvider = leaderboardProvider;
        }
        public async Task<List<Leaderboard>> GetLeaderboard()
        {
            return await _leaderboardProvider.GetLeaderboard();
        }

        public async Task UpsertScore(string Country, int Score)
        {
            await _leaderboardProvider.UpsertScore(Country, Score);
        }
    }
}

using FreddysFunctions.Model;

namespace FreddysFunctions.Service
{
    public interface ILeaderboardService
    {
        Task UpsertScore(string Country,int Score);
        Task<List<Leaderboard>> GetLeaderboard();
    }
}

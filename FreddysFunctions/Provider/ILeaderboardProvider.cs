using FreddysFunctions.Model;

namespace FreddysFunctions.Provider
{
    public interface ILeaderboardProvider
    {
        Task UpsertScore(string Country, int Score);
        Task<List<Leaderboard>> GetLeaderboard();
    }
}

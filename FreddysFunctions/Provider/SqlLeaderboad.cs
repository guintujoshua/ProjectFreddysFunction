using Dapper;
using FreddysFunctions.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FreddysFunctions.Provider
{
    public class SqlLeaderboad : ILeaderboardProvider
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        public SqlLeaderboad(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<Leaderboard>> GetLeaderboard()
        {
            await using SqlConnection connection = _sqlConnectionFactory.CreateConnection();
            var query = await connection.QueryAsync<Leaderboard>("Read_Leaderboards",
                commandType: CommandType.StoredProcedure);

            return query.ToList();
        }

        public async Task UpsertScore(string Country, int Score)
        {
            await using SqlConnection connection = _sqlConnectionFactory.CreateConnection();
            var query = await connection.ExecuteAsync("Upsert_Leaderboards",
                new { Country,
                    Score
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}

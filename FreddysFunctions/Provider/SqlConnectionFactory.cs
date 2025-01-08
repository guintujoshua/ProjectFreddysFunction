using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FreddysFunctions.Provider
{
    public class SqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        // ref: https://learn.microsoft.com/en-us/sql/connect/ado-net/configurable-retry-logic-sqlclient-introduction?view=sql-server-ver16
        private readonly SqlRetryLogicOption options = new SqlRetryLogicOption()
        {
            NumberOfTries = 5,
            DeltaTime = TimeSpan.FromSeconds(5),
            MaxTimeInterval = TimeSpan.FromSeconds(20)
        };

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            var provider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(options);
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.RetryLogicProvider = provider;
            return connection;
        }
    }
}

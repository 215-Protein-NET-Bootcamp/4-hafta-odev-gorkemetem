using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace HomeworkApi.Data
{
    public class DapperDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly string databaseType;
        public DapperDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            databaseType = configuration.GetConnectionString("DbType");
            connectionString = GetConnectionString();
        }
        private string GetConnectionString()
        {
            switch (databaseType)
            {
                case "SQL":
                    return this.configuration.GetConnectionString("DefaultConnection");
                case "Postgre":
                    return this.configuration.GetConnectionString("PostgreSqlConnection");
                default:
                    return this.configuration.GetConnectionString("DefaultConnection");
            }
        }
        public IDbConnection CreateConnection()
        {
            switch (databaseType)
            {
                case "SQL":
                    return new SqlConnection(connectionString);
                case "Postgre":
                    return new NpgsqlConnection(connectionString);
                default:
                    return new SqlConnection(connectionString);
            }
        }
    }
}

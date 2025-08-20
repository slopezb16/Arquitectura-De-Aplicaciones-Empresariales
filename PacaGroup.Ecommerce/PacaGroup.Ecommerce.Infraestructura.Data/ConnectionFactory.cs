using Microsoft.Extensions.Configuration;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Data;
using System.Data.SqlClient;
namespace PacaGroup.Ecommerce.Infrastructure.Data
{
    // Se deja de usar para usar Dapper
    // Seccion 17
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null)
                {
                    return null;
                }
                // Get the connection string from the configuration
                sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                // Open the connection
                sqlConnection.Open();
                // Return the connection
                return sqlConnection;
            }
        }
    }
}

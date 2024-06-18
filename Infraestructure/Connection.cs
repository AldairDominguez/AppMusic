using System;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = configuration.GetConnectionString("AppMusicDb");
        }

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("No se puede conectar a la base de datos.", ex);
            }
        }
    }
}

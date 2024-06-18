using System;
using System.Data.SqlClient;
using Infrastructure;

namespace Application
{
    public class AuthenticationService
    {
        private readonly Connection _connection;

        public AuthenticationService(Connection connection)
        {
            _connection = connection;
        }

        public int Authenticate(string username, string password)
        {
            using (SqlConnection conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Role FROM Users WHERE Username=@username AND Password=@password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        return -1; 
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while authenticating the user.", ex);
                }
            }
        }
    }
}
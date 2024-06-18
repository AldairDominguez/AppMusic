using Application.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace Application.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Connection _connection;

        public AuthenticationService(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
           
        }

        public int Authenticate(string username, string password)
        {
            SqlConnection conn = null;
            try
            {
                conn = _connection.GetConnection();
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
            catch (SqlException sqlEx)
            {
                throw new Exception("Error de SQL: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al autenticar al usuario: " + ex.Message, ex);
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}

using CrossCutting.DTO;
using Infraestructure.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infraestructure.Implementations
{
    public class WeatherSQL : IWeatherSQL
    {
        private readonly Connection _connection;

        public WeatherSQL(Connection connection)
        {
            _connection = connection;
        }

        public async Task Add(WeatherDTO weather)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Weathers (Id, Code, Description) VALUES (@Id, @Code, @Description)", conn);
                command.Parameters.AddWithValue("@Id", weather.Id);
                command.Parameters.AddWithValue("@Code", weather.Code);
                command.Parameters.AddWithValue("@Description", weather.Description);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(WeatherDTO weather)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Weathers SET Code = @Code, Description = @Description WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", weather.Id);
                command.Parameters.AddWithValue("@Code", weather.Code);
                command.Parameters.AddWithValue("@Description", weather.Description);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Weathers WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<WeatherDTO> GetById(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Weathers WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new WeatherDTO
                        {
                            Id = (int)reader["Id"],
                            Code = reader["Code"].ToString(),
                            Description = reader["Description"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<WeatherDTO>> GetAll()
        {
            var weathers = new List<WeatherDTO>();

            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Weathers", conn);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        weathers.Add(new WeatherDTO
                        {
                            Id = (int)reader["Id"],
                            Code = reader["Code"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }

            return weathers;
        }


        public async Task<int> GetNextWeatherIdAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM Weathers", conn);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
        public async Task<Dictionary<int, string>> GetWeatherIdsAndCodesAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT Id, Code FROM Weathers", conn);
                var reader = await command.ExecuteReaderAsync();
                var weatherData = new Dictionary<int, string>();
                while (await reader.ReadAsync())
                {
                    weatherData.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return weatherData;
            }
        }

        public async Task<WeatherDTO> GetWeatherByIdAsync(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Weathers WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new WeatherDTO
                            {
                                Id = (int)reader["Id"],
                                Code = (string)reader["Code"],
                                Description = (string)reader["Description"]
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}


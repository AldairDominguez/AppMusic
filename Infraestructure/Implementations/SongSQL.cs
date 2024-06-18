using CrossCutting.DTO;
using Infraestructure.Interfaces;
using Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;

namespace Infraestructure.Implementations
{
    public class SongSQL : ISongSQL
    {
        private readonly Connection _connection;

        public SongSQL(Connection connection)
        {
            _connection = connection;
        }

        public async Task Add(SongDTO song)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Songs (Id, Name, Category, AuthorId, Album, Duration) VALUES (@Id, @Name, @Category, @AuthorId, @Album, @Duration)", conn);
                command.Parameters.AddWithValue("@Id", song.Id);
                command.Parameters.AddWithValue("@Name", song.Name);
                command.Parameters.AddWithValue("@Category", song.Category);
                command.Parameters.AddWithValue("@AuthorId", song.AuthorId);
                command.Parameters.AddWithValue("@Album", song.Album);
                command.Parameters.AddWithValue("@Duration", song.Duration);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(SongDTO song)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Songs SET Name = @Name, Category = @Category, AuthorId = @AuthorId, Album = @Album, Duration = @Duration WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Name", song.Name);
                command.Parameters.AddWithValue("@Category", song.Category);
                command.Parameters.AddWithValue("@AuthorId", song.AuthorId);
                command.Parameters.AddWithValue("@Album", song.Album);
                command.Parameters.AddWithValue("@Duration", song.Duration);
                command.Parameters.AddWithValue("@Id", song.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Remove(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Songs WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<SongDTO> Get(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Songs WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new SongDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Category = reader.GetString(2),
                            AuthorId = reader.GetInt32(3),
                            Album = reader.GetString(4),
                            Duration = reader.GetTimeSpan(5)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<SongDTO>> GetAll()
        {
            var songs = new List<SongDTO>();

            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Songs", conn);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        songs.Add(new SongDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Category = reader.GetString(2),
                            AuthorId = reader.GetInt32(3),
                            Album = reader.GetString(4),
                            Duration = reader.GetTimeSpan(5)
                        });
                    }
                }
            }

            return songs;
        }
        public async Task<int> GetNextIdAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM Songs", conn);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }
}

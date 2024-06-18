using CrossCutting.DTO;
using Infraestructure.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infraestructure.Implementations
{
    public class PlaylistSQL : IPlaylistSQL
    {
        private readonly Connection _connection;

        public PlaylistSQL(Connection connection)
        {
            _connection = connection;
        }

        public async Task<int> Add(PlaylistDTO playlist)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Playlists (Id, Name, WeatherId) VALUES (@Id, @Name, @WeatherId)", conn);
                int newId = await GetNextIdAsync();
                command.Parameters.AddWithValue("@Id", newId);
                command.Parameters.AddWithValue("@Name", playlist.Name);
                command.Parameters.AddWithValue("@WeatherId", playlist.WeatherId);

                await command.ExecuteNonQueryAsync();
                return newId;
            }
        }

        public async Task Update(PlaylistDTO playlist)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Playlists SET Name = @Name, WeatherId = @WeatherId WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Name", playlist.Name);
                command.Parameters.AddWithValue("@WeatherId", playlist.WeatherId);
                command.Parameters.AddWithValue("@Id", playlist.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Playlists WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<PlaylistDTO> GetById(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Playlists WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new PlaylistDTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            WeatherId = reader.GetInt32(reader.GetOrdinal("WeatherId"))
                        };
                    }
                }
            }
            return null;
        }

        public async Task<int> GetNextIdAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM Playlists", conn);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> GetNextPlaylistIdAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM Playlists", conn);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<PlaylistDTO>> GetAllPlaylistsAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT Id, Name, WeatherId FROM Playlists", conn);
                var reader = await command.ExecuteReaderAsync();

                var playlists = new List<PlaylistDTO>();

                while (await reader.ReadAsync())
                {
                    var playlist = new PlaylistDTO
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        WeatherId = reader.GetInt32(2)
                    };
                    playlists.Add(playlist);
                }

                return playlists;
            }
        }




    }
}

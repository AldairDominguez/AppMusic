using CrossCutting.DTO;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Infraestructure.Interfaces;

namespace Infraestructure.Implementations
{
    public class PlaylistSongsSQL : IPlaylistSongsSQL
    {
        private readonly Connection _connection;

        public PlaylistSongsSQL(Connection connection)
        {
            _connection = connection;
        }

        public async Task Add(PlaylistSongsDTO playlistSongs)
        {
            using (var db = _connection.GetConnection())
            {
                var query = "INSERT INTO PlaylistSongs (PlaylistId, SongId) VALUES (@PlaylistId, @SongId)";
                await db.ExecuteAsync(query, playlistSongs);
            }
        }

        public async Task Remove(PlaylistSongsDTO playlistSongs)
        {
            using (var db = _connection.GetConnection())
            {
                var query = "DELETE FROM PlaylistSongs WHERE PlaylistId = @PlaylistId AND SongId = @SongId";
                await db.ExecuteAsync(query, playlistSongs);
            }
        }

        public async Task<List<SongDTO>> GetSongsByPlaylistId(int playlistId)
        {
            using (var db = _connection.GetConnection())
            {
                var query = "SELECT s.* FROM Songs s INNER JOIN PlaylistSongs ps ON s.Id = ps.SongId WHERE ps.PlaylistId = @PlaylistId";
                return (await db.QueryAsync<SongDTO>(query, new { PlaylistId = playlistId })).ToList();
            }
        }

        public async Task<List<SongDTO>> GetSongsWithAuthorsByPlaylistIdAsync(int playlistId)
        {
            var songs = new List<SongDTO>();
            using (var connection = _connection.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                    "SELECT s.*, a.Name as AuthorName " +
                    "FROM Songs s " +
                    "INNER JOIN Authors a ON s.AuthorId = a.Id " +
                    "INNER JOIN PlaylistSongs ps ON s.Id = ps.SongId " +
                    "WHERE ps.PlaylistId = @PlaylistId",
                    connection))
                {
                    command.Parameters.AddWithValue("@PlaylistId", playlistId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var song = new SongDTO
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Category = (string)reader["Category"],
                                AuthorId = (int)reader["AuthorId"],
                                AuthorName = (string)reader["AuthorName"],
                                Album = (string)reader["Album"],
                                Duration = (TimeSpan)reader["Duration"]
                            };
                            songs.Add(song);
                        }
                    }
                }
            }
            return songs;
        }
        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            using (var connection = _connection.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Authors WHERE Id = @AuthorId", connection))
                {
                    command.Parameters.AddWithValue("@AuthorId", authorId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new AuthorDTO
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };
                        }
                    }
                }
            }
            return null;
        }

    }
}

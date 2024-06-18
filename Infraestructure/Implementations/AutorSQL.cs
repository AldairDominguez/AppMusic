using CrossCutting.DTO;
using Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Infraestructure.Interfaces;
using System;

namespace Infraestructure.Implementations
{
    public class AutorSQL : IAutorSQL<AuthorDTO>
    {
        private readonly Connection _connection;

        public AutorSQL(Connection connection)
        {
            _connection = connection;
        }

        public async Task Add(AuthorDTO author)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Authors (Id, Name) VALUES (@Id, @Name)", conn);
                command.Parameters.AddWithValue("@Id", author.Id);
                command.Parameters.AddWithValue("@Name", author.Name);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Remove(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Authors WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(AuthorDTO author)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("UPDATE Authors SET Name = @Name WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Name", author.Name);
                command.Parameters.AddWithValue("@Id", author.Id);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<AuthorDTO> Get(int id)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Authors WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new AuthorDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<AuthorDTO>> GetAll()
        {
            var result = new List<AuthorDTO>();
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Authors", conn);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var author = new AuthorDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        result.Add(author);
                    }
                }
            }
            return result;
        }
        public async Task<int> GetNextIdAsync()
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT ISNULL(MAX(Id), 0) + 1 FROM Authors", conn);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
        public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
        {
            var result = new List<AuthorDTO>();
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT Id, Name FROM Authors", conn);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var author = new AuthorDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        result.Add(author);
                    }
                }
            }
            return result;
        }
        public async Task<AuthorDTO> GetByName(string name)
        {
            using (var conn = _connection.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Authors WHERE Name = @Name", conn);
                command.Parameters.AddWithValue("@Name", name);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new AuthorDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
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


using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorApplication
    {

        Task AddAuthor(AuthorDTO author);
        Task UpdateAuthor(AuthorDTO author);
        Task DeleteAuthor(int id);
        Task<List<AuthorDTO>> GetAllAuthors();
        Task<AuthorDTO> GetAuthorById(int id);
        Task<AuthorDTO> GetAuthorByIdAsync(int authorId);
        Task<int> GetOrCreateAuthorIdAsync(string authorName);
    }
}

using Application.Interfaces;
using CrossCutting.DTO;
using Infraestructure.Implementations;
using Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class AuthorApplication : IAuthorApplication
    {
        private readonly IAutorSQL<AuthorDTO> _comandoSQL;
        

        public AuthorApplication(IAutorSQL<AuthorDTO> comandoSQL)
        {
            _comandoSQL = comandoSQL;
            
        }

        public async Task AddAuthor(AuthorDTO author)
        {
            await _comandoSQL.Add(author);
        }

        public async Task UpdateAuthor(AuthorDTO author)
        {
            await _comandoSQL.Update(author);
        }

        public async Task DeleteAuthor(int id)
        {
            await _comandoSQL.Remove(id);
        }

        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            return await _comandoSQL.GetAll();
        }

        public async Task<AuthorDTO> GetAuthorById(int id)
        {
            return await _comandoSQL.Get(id);
        }
        public async Task<int> GetOrCreateAuthorIdAsync(string authorName)
        {
            var author = await _comandoSQL.GetByName(authorName);
            if (author != null)
            {
                return author.Id;
            }
            else
            {
                var newAuthor = new AuthorDTO { Name = authorName };
                await _comandoSQL.Add(newAuthor);
                return newAuthor.Id;
            }
        }
        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            return await _comandoSQL.GetAuthorByIdAsync(authorId);
        }
    }
}

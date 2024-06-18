using CrossCutting.DTO;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    
    public interface IAutorSQL<T>
    {
        Task Add(T entity);
        Task Remove(int id);
        Task Update(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();

        Task<T> GetByName(string name);
        Task<AuthorDTO> GetAuthorByIdAsync(int authorId);


    }
    
}

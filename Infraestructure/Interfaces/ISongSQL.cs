using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface ISongSQL
    {
        Task Add(SongDTO song);
        Task Update(SongDTO song);
        Task Remove(int id);
        Task<SongDTO> Get(int id);
        Task<IEnumerable<SongDTO>> GetAll();
        Task<int> GetNextIdAsync();
    }
}

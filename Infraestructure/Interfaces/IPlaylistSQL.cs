using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IPlaylistSQL
    {
        
        Task Update(PlaylistDTO playlist);
        Task Delete(int id);
        Task<PlaylistDTO> GetById(int id);
        Task<int> GetNextIdAsync();
        Task<int> Add(PlaylistDTO playlist);

        Task<List<PlaylistDTO>> GetAllPlaylistsAsync();
    }
}

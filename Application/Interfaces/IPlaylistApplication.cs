using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlaylistApplication
    {
        Task AddPlaylistAsync(PlaylistDTO playlist);
        Task UpdatePlaylistAsync(PlaylistDTO playlist);
        Task DeletePlaylistAsync(int id);
        Task<PlaylistDTO> GetPlaylistByIdAsync(int id);
        Task<int> GetNextPlaylistIdAsync();
        Task<int> Add(PlaylistDTO playlist);

        Task<List<PlaylistDTO>> GetAllPlaylistsAsync();

        Task<bool> PlaylistNameExistsAsync(string name);

    }
}

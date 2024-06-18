using CrossCutting.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISongApplication
    {
        Task AddSongAsync(SongDTO song);
        Task UpdateSongAsync(SongDTO song);
        Task RemoveSongAsync(int id);
        Task<SongDTO> GetSongAsync(int id);
        Task<IEnumerable<SongDTO>> GetAllSongsAsync();
        Task<int> GetNextIdAsync();

        Task AddSongWithFileAsync(SongDTO song, string sourceFilePath);

    }
}

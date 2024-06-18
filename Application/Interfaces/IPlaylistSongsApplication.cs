using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlaylistSongsApplication
    {
        Task AddSongToPlaylistAsync(PlaylistSongsDTO playlistSongs);
        Task RemoveSongFromPlaylistAsync(PlaylistSongsDTO playlistSongs);
        Task<List<SongDTO>> GetSongsByPlaylistIdAsync(int playlistId);

        Task<List<SongDTO>> GetSongsWithAuthorsByPlaylistIdAsync(int playlistId);
    }
}

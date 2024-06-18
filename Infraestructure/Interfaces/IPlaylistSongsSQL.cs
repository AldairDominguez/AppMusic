using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IPlaylistSongsSQL
    {
        Task Add(PlaylistSongsDTO playlistSongs);
        Task Remove(PlaylistSongsDTO playlistSongs);
        Task<List<SongDTO>> GetSongsByPlaylistId(int playlistId);

        Task<List<SongDTO>> GetSongsWithAuthorsByPlaylistIdAsync(int playlistId);
    }
}

using Application.Interfaces;
using CrossCutting.DTO;
using Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class PlaylistApplication : IPlaylistApplication
    {
        
        private readonly IPlaylistSQL _playlistSQL;
        private readonly IWeatherApplication _weatherApplication;

        public PlaylistApplication(IPlaylistSQL playlistSQL, IWeatherApplication weatherApplication)
        {
            _playlistSQL = playlistSQL;
            _weatherApplication = weatherApplication;
            
        }

        public async Task AddPlaylistAsync(PlaylistDTO playlist)
        {
            await _playlistSQL.Add(playlist);
            
        }

        public async Task UpdatePlaylistAsync(PlaylistDTO playlist)
        {
            await _playlistSQL.Update(playlist);
        }

        public async Task DeletePlaylistAsync(int id)
        {
            await _playlistSQL.Delete(id);
        }

        public async Task<PlaylistDTO> GetPlaylistByIdAsync(int id)
        {
            return await _playlistSQL.GetById(id);
        }

        public async Task<int> GetNextPlaylistIdAsync()
        {
            return await _playlistSQL.GetNextIdAsync();
        }
        public async Task<int> Add(PlaylistDTO playlist)
        {
            return await _playlistSQL.Add(playlist);
        }

        public async Task<List<PlaylistDTO>> GetAllPlaylistsAsync()
        {
            return await _playlistSQL.GetAllPlaylistsAsync();
        }

        public async Task<bool> PlaylistNameExistsAsync(string name)
        {
            var playlists = await GetAllPlaylistsAsync();
            return playlists.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
       
    }
}

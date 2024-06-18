using Application.Interfaces;
using CrossCutting.DTO;
using Infraestructure.Implementations;
using Infraestructure.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class SongApplication : ISongApplication
    {
        private readonly ISongSQL _songSQL;
        

        public SongApplication(ISongSQL songSQL)
        {
            _songSQL = songSQL;
            
        }

        public async Task AddSongAsync(SongDTO song)
        {
            await _songSQL.Add(song);
            
        }

        public async Task UpdateSongAsync(SongDTO song)
        {
            await _songSQL.Update(song);
        }

        public async Task RemoveSongAsync(int id)
        {
            await _songSQL.Remove(id);
        }

        public async Task<SongDTO> GetSongAsync(int id)
        {
            return await _songSQL.Get(id);
        }

        public async Task<IEnumerable<SongDTO>> GetAllSongsAsync()
        {
            return await _songSQL.GetAll();
        }
        public async Task<int> GetNextIdAsync()
        {
            return await _songSQL.GetNextIdAsync();
        }
        public async Task AddSongWithFileAsync(SongDTO song, string sourceFilePath)
        {
            string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica");
            if (!Directory.Exists(musicFolderPath))
            {
                Directory.CreateDirectory(musicFolderPath);
            }
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException("El archivo de origen no existe.");
            }
            string fileName = Path.GetFileName(sourceFilePath);
            string destinationPath = Path.Combine(musicFolderPath, fileName);
            File.Copy(sourceFilePath, destinationPath, true);
            song.Id = await GetNextIdAsync();
            await AddSongAsync(song);
        }

    }
}

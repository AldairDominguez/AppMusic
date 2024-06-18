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
    public class PlaylistSongsApplication : IPlaylistSongsApplication
    {
        private readonly IPlaylistSongsSQL _playlistSongsSQL;
        private readonly ISongApplication _songApplication;
        private readonly IPlaylistApplication _playlistApplication;

        public PlaylistSongsApplication(IPlaylistSongsSQL playlistSongsSQL, ISongApplication songApplication, IPlaylistApplication playlistApplication)
        {
            _playlistSongsSQL = playlistSongsSQL;
            _songApplication = songApplication;
            _playlistApplication = playlistApplication;
        }

        public async Task AddSongToPlaylistAsync(PlaylistSongsDTO playlistSongs)
        {
            await _playlistSongsSQL.Add(playlistSongs);
            
        }

        public async Task RemoveSongFromPlaylistAsync(PlaylistSongsDTO playlistSongs)
        {
            await _playlistSongsSQL.Remove(playlistSongs);
            
        }

        public async Task<List<SongDTO>> GetSongsByPlaylistIdAsync(int playlistId)
        {
            return await _playlistSongsSQL.GetSongsByPlaylistId(playlistId);
           
        }
        private async Task CopySongToPlaylistFolder(PlaylistSongsDTO playlistSongs)
        {
            var song = await _songApplication.GetSongAsync(playlistSongs.SongId);
            var playlist = await _playlistApplication.GetPlaylistByIdAsync(playlistSongs.PlaylistId);
            string songFileName = $"{song.Name}.mp3";
            string songFilePath = Path.Combine("Musica", songFileName);

            if (File.Exists(songFilePath))
            {
                string playListFolder = Path.Combine("Playlist", playlist.Name);
                Directory.CreateDirectory(playListFolder);
                string destinationFilePath = Path.Combine(playListFolder, songFileName);

                File.Copy(songFilePath, destinationFilePath, true);
            }
            else
            {
                throw new FileNotFoundException("El archivo de música no se encontró.", songFilePath);
            }
        }
        private async Task RemoveSongFromPlaylistFolder(PlaylistSongsDTO playlistSongs)
        {
            var song = await _songApplication.GetSongAsync(playlistSongs.SongId);
            var playlist = await _playlistApplication.GetPlaylistByIdAsync(playlistSongs.PlaylistId);
            string songFileName = $"{song.Name}.mp3";
            string playListFolder = Path.Combine("Playlist", playlist.Name);
            string destinationFilePath = Path.Combine(playListFolder, songFileName);

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }
        }
        public async Task<List<SongDTO>> GetSongsWithAuthorsByPlaylistIdAsync(int playlistId)
        {
            return await _playlistSongsSQL.GetSongsWithAuthorsByPlaylistIdAsync(playlistId);
        }

    }
}

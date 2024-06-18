using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTO
{
    public static class PlaylistFolderHelper
    {
        private static string GetMusicFolderPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Playlist");
        }

        private static string GetPlaylistFolderPath(string playlistName)
        {
            return Path.Combine(GetMusicFolderPath(), playlistName);
        }

        public static void EnsureMusicFolderExists()
        {
            string musicFolderPath = GetMusicFolderPath();

            if (!Directory.Exists(musicFolderPath))
            {
                Directory.CreateDirectory(musicFolderPath);
            }
        }

        public static void CreatePlaylistFolder(string playlistName)
        {
            string playlistFolderPath = GetPlaylistFolderPath(playlistName);

            if (!Directory.Exists(playlistFolderPath))
            {
                Directory.CreateDirectory(playlistFolderPath);
            }
        }

        public static void RenamePlaylistFolder(string oldName, string newName)
        {
            string oldPath = GetPlaylistFolderPath(oldName);
            string newPath = GetPlaylistFolderPath(newName);

            if (Directory.Exists(oldPath) && !Directory.Exists(newPath))
            {
                Directory.Move(oldPath, newPath);
            }
        }

        public static void DeletePlaylistFolder(string playlistName)
        {
            string playlistFolderPath = GetPlaylistFolderPath(playlistName);

            if (Directory.Exists(playlistFolderPath))
            {
                Directory.Delete(playlistFolderPath, true);
            }
        }

    }
}

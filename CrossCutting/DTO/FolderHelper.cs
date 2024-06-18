using System;
using System.IO;

namespace CrossCutting.DTO
{
    public static class FolderHelper
    {
        public static void EnsureMusicFolderExists()
        {
            string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica");

            if (!Directory.Exists(musicFolderPath))
            {
                Directory.CreateDirectory(musicFolderPath);
            }
        }
    }
}

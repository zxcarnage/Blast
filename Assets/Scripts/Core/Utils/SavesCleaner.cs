using System.IO;
using UnityEngine;

namespace Core.Utils
{
    public static class SavesCleaner
    {
        public static void DeleteAllSaveFiles()
        {
            var savePath = Application.persistentDataPath;

            if (string.IsNullOrEmpty(savePath))
                return;

            if (savePath.EndsWith("/"))
                savePath = savePath.Substring(0, savePath.Length - 1);

            if (Directory.Exists(savePath))
            {
                DeleteDirectory(savePath);
            }
        }

        private static void DeleteDirectory(string dirPath)
        {
            var directory = new DirectoryInfo(dirPath);

            foreach (var file in directory.GetFiles())
            {
                file.IsReadOnly = false;
                file.Delete();
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                DeleteDirectory(subDirectory.FullName);
            }
        }
    }
}



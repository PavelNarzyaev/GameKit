using System.IO;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class FilePlayerStateStorage : IPlayerStateStorage
    {
        private const string k_FileName = "state.dat";
        private readonly string m_storageDirectoryPath;

        public FilePlayerStateStorage()
            : this(Application.persistentDataPath)
        {
        }

        private FilePlayerStateStorage(string storageDirectoryPath)
        {
            m_storageDirectoryPath = storageDirectoryPath;
        }

        public static FilePlayerStateStorage CreateForDirectory(string storageDirectoryPath)
        {
            return new FilePlayerStateStorage(storageDirectoryPath);
        }

        public bool Exists()
        {
            return File.Exists(GetFilePath());
        }

        public void Save(string stateJson)
        {
            var filePath = GetFilePath();
            var temporaryFilePath = $"{filePath}.tmp";

            File.WriteAllText(temporaryFilePath, stateJson);
            if (File.Exists(filePath))
            {
                File.Replace(temporaryFilePath, filePath, null);
                return;
            }

            File.Move(temporaryFilePath, filePath);
        }

        public string Load()
        {
            return File.ReadAllText(GetFilePath());
        }

        public void Delete()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("Player state file deleted.");
            }
            else
            {
                Debug.LogWarning("Player state file not found.");
            }
        }

        private string GetFilePath()
        {
            return Path.Combine(m_storageDirectoryPath, k_FileName);
        }
    }
}

using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class FilePlayerStateStorage : IPlayerStateStorage
    {
        private const string k_FileName = "state.dat";

        public bool Exists()
        {
            return File.Exists(GetFilePath());
        }

        public void Save(string stateJson)
        {
            File.WriteAllText(GetFilePath(), stateJson);
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

        private static string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, k_FileName);
        }
    }
}

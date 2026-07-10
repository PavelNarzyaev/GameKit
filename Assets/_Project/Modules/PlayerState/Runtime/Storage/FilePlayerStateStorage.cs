using System.IO;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class FilePlayerStateStorage : IPlayerStateStorage
    {
        private const string k_FileName = "state.dat";
        private readonly string m_storageDirectoryPath;
        private readonly IPlayerStateSerializer m_playerStateSerializer;

        public FilePlayerStateStorage()
            : this(Application.persistentDataPath, new JsonPlayerStateSerializer())
        {
        }

        [Inject]
        public FilePlayerStateStorage(IPlayerStateSerializer playerStateSerializer)
            : this(Application.persistentDataPath, playerStateSerializer)
        {
        }

        private FilePlayerStateStorage(string storageDirectoryPath, IPlayerStateSerializer playerStateSerializer)
        {
            m_storageDirectoryPath = storageDirectoryPath;
            m_playerStateSerializer = playerStateSerializer;
        }

        public static FilePlayerStateStorage CreateForDirectory(string storageDirectoryPath)
        {
            return new FilePlayerStateStorage(storageDirectoryPath, new JsonPlayerStateSerializer());
        }

        public bool Exists()
        {
            return File.Exists(GetFilePath());
        }

        public void Save(PlayerStateDto state)
        {
            SaveContent(m_playerStateSerializer.Serialize(state));
        }

        public PlayerStateDto Load()
        {
            return m_playerStateSerializer.Deserialize(LoadContent());
        }

        internal void SaveContent(string content)
        {
            var filePath = GetFilePath();
            var temporaryFilePath = $"{filePath}.tmp";

            File.WriteAllText(temporaryFilePath, content);
            if (File.Exists(filePath))
            {
                File.Replace(temporaryFilePath, filePath, null);
                return;
            }

            File.Move(temporaryFilePath, filePath);
        }

        internal string LoadContent()
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

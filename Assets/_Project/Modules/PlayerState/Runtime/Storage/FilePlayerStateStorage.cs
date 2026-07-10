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
        private readonly IPlayerStateCodec m_playerStateCodec;

        public FilePlayerStateStorage()
            : this(Application.persistentDataPath, CreateDefaultCodec())
        {
        }

        [Inject]
        public FilePlayerStateStorage(IPlayerStateCodec playerStateCodec)
            : this(Application.persistentDataPath, playerStateCodec)
        {
        }

        private FilePlayerStateStorage(string storageDirectoryPath, IPlayerStateCodec playerStateCodec)
        {
            m_storageDirectoryPath = storageDirectoryPath;
            m_playerStateCodec = playerStateCodec;
        }

        public static FilePlayerStateStorage CreateForDirectory(string storageDirectoryPath)
        {
            return new FilePlayerStateStorage(storageDirectoryPath, CreateDefaultCodec());
        }

        public bool Exists()
        {
            return File.Exists(GetFilePath());
        }

        public void Save(PlayerStateDto state)
        {
            var filePath = GetFilePath();
            var temporaryFilePath = $"{filePath}.tmp";

            File.WriteAllText(temporaryFilePath, m_playerStateCodec.Encode(state));
            if (File.Exists(filePath))
            {
                File.Replace(temporaryFilePath, filePath, null);
                return;
            }

            File.Move(temporaryFilePath, filePath);
        }

        public PlayerStateDto Load()
        {
            return m_playerStateCodec.Decode(File.ReadAllText(GetFilePath()));
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

        private static IPlayerStateCodec CreateDefaultCodec()
        {
            return new FilePlayerStateCodec(new JsonPlayerStateSerializer(), new AesTextCipher());
        }
    }
}

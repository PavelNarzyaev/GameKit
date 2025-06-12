using System;
using System.IO;
using Data;
using JetBrains.Annotations;
using UnityEngine;
using Utilities;

namespace Proxies
{
    [UsedImplicitly]
    public class LocalStateProxy
    {
        private const string k_FileName = "state.json";

        public State Data;
        public bool IsDirty { get; private set; }
        public event Action RefreshFromJsonEvent;

        private static string GetFilePath()
        {
            return Path.Combine(Application.persistentDataPath, k_FileName);
        }

        public void MarkAsDirty()
        {
            IsDirty = true;
        }

        public void Save()
        {
            SaveJsonToFile(JsonUtility.ToJson(Data));
        }

        public void Set(string json)
        {
            try
            {
                Data = JsonUtility.FromJson<State>(json);
            }
            catch
            {
                // TODO: show loading error popup
                Debug.Log("Failed to load state");
            }

            SaveJsonToFile(json);
            RefreshFromJsonEvent?.Invoke();
        }

        private void SaveJsonToFile(string json)
        {
            var encryptedJson = EncryptionUtility.Encrypt(json);
            File.WriteAllText(GetFilePath(), encryptedJson);
            IsDirty = false;
        }

        public bool CheckIfExists()
        {
            return File.Exists(GetFilePath());
        }

        public void Refresh()
        {
            var json = Get();
            Data = JsonUtility.FromJson<State>(json);
        }

        public string Get()
        {
            var encryptedJson = File.ReadAllText(GetFilePath());
            return EncryptionUtility.Decrypt(encryptedJson);
        }

        public static void Delete()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("Persistent data file deleted.");
            }
            else
            {
                Debug.LogWarning("Persistent data file not found.");
            }
        }
    }
}

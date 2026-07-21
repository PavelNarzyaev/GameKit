using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class AudioSourceFactory
    {
        private const string k_RootName = "GameKitAudio";

        private GameObject m_root;

        public AudioSource Create(string sourceName)
        {
            EnsureRootCreated();

            var gameObject = new GameObject(sourceName);
            gameObject.transform.SetParent(m_root.transform);

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;

            return audioSource;
        }

        private void EnsureRootCreated()
        {
            if (m_root)
            {
                return;
            }

            m_root = new GameObject(k_RootName);
            Object.DontDestroyOnLoad(m_root);
        }
    }
}

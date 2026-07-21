using UnityEngine;

namespace GameKit.Audio.Contracts
{
    public interface IMusicPlayer
    {
        void Play(AudioClip clip);
        void Stop();
    }
}

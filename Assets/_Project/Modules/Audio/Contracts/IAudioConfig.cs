using UnityEngine;

namespace GameKit.Audio.Contracts
{
    public interface IAudioConfig
    {
        AudioClip BackgroundMusic { get; }
        AudioClip ButtonClick { get; }
    }
}

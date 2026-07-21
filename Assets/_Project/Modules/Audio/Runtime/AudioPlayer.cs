using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameKit.Audio
{
    [UsedImplicitly]
    public class AudioPlayer
    {
        internal void EnsureClipConfigured(AudioClip clip)
        {
            if (clip)
            {
                return;
            }

            throw new InvalidOperationException("Audio clip is not configured.");
        }

        internal void PlayLooped(AudioSource audioSource, AudioClip clip)
        {
            EnsureClipConfigured(clip);

            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
            }

            audioSource.loop = true;

            if (audioSource.isPlaying)
            {
                return;
            }

            audioSource.Play();
        }

        internal void PlayOneShot(AudioSource audioSource, AudioClip clip)
        {
            EnsureClipConfigured(clip);
            audioSource.PlayOneShot(clip);
        }

        internal void Pause(AudioSource audioSource)
        {
            if (!audioSource.isPlaying)
            {
                return;
            }

            audioSource.Pause();
        }

        internal void UnPause(AudioSource audioSource)
        {
            if (audioSource.isPlaying)
            {
                return;
            }

            audioSource.UnPause();
        }

        internal void Stop(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}

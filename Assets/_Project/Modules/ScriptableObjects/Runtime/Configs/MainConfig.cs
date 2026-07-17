using GameKit.Audio.Contracts;
using GameKit.Core;
using GameKit.Energy.Contracts;
using UnityEngine;

namespace GameKit.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(MainConfig), menuName = AssetMenuConstants.k_Configs + "/" + nameof(MainConfig))]
    public class MainConfig : ScriptableObject, IEnergyConfig, IAudioConfig
    {
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private int oneEnergyRestorationSeconds = 60;
        [SerializeField] private int energyRestorationLimit = 100;

        public AudioClip BackgroundMusic => backgroundMusic;
        public AudioClip ButtonClick => buttonClick;
        public int OneEnergyRestorationSeconds => oneEnergyRestorationSeconds;
        public int EnergyRestorationLimit => energyRestorationLimit;
    }
}

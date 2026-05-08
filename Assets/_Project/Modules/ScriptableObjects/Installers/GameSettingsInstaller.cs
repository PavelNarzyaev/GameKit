using GameKit.Core;
using GameKit.Energy;
using UnityEngine;
using Zenject;

namespace GameKit.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller), menuName = AssetMenuConstants.k_Installers + "/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private MainConfig mainConfig;

        public override void InstallBindings()
        {
            Container.Bind(typeof(MainConfig), typeof(IEnergyConfig)).FromInstance(mainConfig);
        }
    }
}

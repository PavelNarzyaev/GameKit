using ScriptableObjects.Configs;
using UnityEngine;
using Zenject;

namespace ScriptableObjects.Installers
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller), menuName = EditorMenuConstants.Installers + "/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private MainConfig mainConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(mainConfig).AsSingle();
        }
    }
}

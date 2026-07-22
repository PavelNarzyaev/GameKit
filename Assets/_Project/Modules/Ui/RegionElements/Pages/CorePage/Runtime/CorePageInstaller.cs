using UnityEngine;
using Zenject;

namespace GameKit.CorePage
{
    [CreateAssetMenu(fileName = nameof(CorePageInstaller), menuName = "Installers/" + nameof(CorePageInstaller))]
    public class CorePageInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CorePagePresenter>().AsSingle();
        }
    }
}

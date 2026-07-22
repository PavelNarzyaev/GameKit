using UnityEngine;
using Zenject;

namespace GameKit.MetaPage
{
    [CreateAssetMenu(fileName = nameof(MetaPageInstaller), menuName = "Installers/" + nameof(MetaPageInstaller))]
    public class MetaPageInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MetaPagePresenter>().AsSingle();
        }
    }
}

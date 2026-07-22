using UnityEngine;
using Zenject;

namespace GameKit.TopPanel
{
    [CreateAssetMenu(fileName = nameof(TopPanelInstaller), menuName = "Installers/" + nameof(TopPanelInstaller))]
    public class TopPanelInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TopPanelPresenter>().AsSingle();
        }
    }
}

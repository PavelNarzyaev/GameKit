using UnityEngine;
using Zenject;

namespace GameKit.ProductionMode
{
    [CreateAssetMenu(fileName = nameof(ProductionModeInstaller), menuName = "Installers/" + nameof(ProductionModeInstaller))]
    public class ProductionModeInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();
        }
    }
}

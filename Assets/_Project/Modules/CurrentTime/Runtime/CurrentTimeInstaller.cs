using GameKit.Core.Contracts;
using UnityEngine;
using Zenject;

namespace GameKit.CurrentTime
{
    [CreateAssetMenu(fileName = nameof(CurrentTimeInstaller), menuName = "Installers/" + nameof(CurrentTimeInstaller))]
    public class CurrentTimeInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
#if IS_PRODUCTION
            Container.Bind(typeof(IRealTimeSource), typeof(ICurrentTimeSource)).To<SystemUtcCurrentTimeSource>().AsSingle();
#else
            Container.Bind<IRealTimeSource>().To<SystemUtcCurrentTimeSource>().AsSingle();
#endif
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();
        }
    }
}

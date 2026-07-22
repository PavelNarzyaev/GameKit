using GameKit.Core.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.TimeOffset
{
    [UsedImplicitly]
    public class TimeOffsetInstaller : Installer<TimeOffsetInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerStateTimeOffsetGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeOffsetService>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
        }
    }
}

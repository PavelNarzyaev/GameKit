using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(MainInstaller), menuName = "Installers/" + nameof(MainInstaller))]
public class MainInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        // proxies
        Container.Bind<LocalStateProxy>().AsSingle();
        Container.Bind<StateClipboardProxy>().AsSingle();
        Container.Bind<CurrentTimeProxy>().AsSingle();
        Container.Bind<PopupsLayerMediator>().AsSingle();

        // mediators
        Container.Bind<LayersMediator>().AsSingle();
        Container.Bind<PagesLayerMediator>().AsSingle();

        // commands
        Container.Bind<InitializeStateCommand>().AsSingle();
        Container.Bind<LaunchCommand>().AsSingle();
        Container.Bind<ResetUiCommand>().AsSingle();
        Container.Bind<ResetStateCommand>().AsSingle();

        // controllers
        Container.BindInterfacesAndSelfTo<StateSavingController>().AsSingle();
        Container.Bind<ResetUiController>().AsSingle().NonLazy();
        Container.BindInterfacesTo<EntryPointController>().AsSingle(); // entry point
    }
}

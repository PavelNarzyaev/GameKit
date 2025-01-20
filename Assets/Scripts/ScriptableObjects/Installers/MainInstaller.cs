using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(MainInstaller), menuName = "Installers/" + nameof(MainInstaller))]
public class MainInstaller : ScriptableObjectInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<StateProxy>().AsSingle();
		Container.Bind<StateClipboardProxy>().AsSingle();
		Container.Bind<CurrentTimeProxy>().AsSingle();

		Container.Bind<InitializeStateCommand>().AsSingle();
		Container.Bind<LaunchCommand>().AsSingle();
		Container.Bind<ResetUiCommand>().AsSingle();
		Container.Bind<ResetStateCommand>().AsSingle();

		Container.BindInterfacesAndSelfTo<StateSavingController>().AsSingle();
		Container.Bind<ResetUiController>().AsSingle().NonLazy();

		Container.Bind<LayersMediator>().AsSingle();
		Container.Bind<PagesLayerMediator>().AsSingle();
	}
}

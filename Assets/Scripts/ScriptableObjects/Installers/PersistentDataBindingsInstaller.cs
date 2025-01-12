using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(PersistentDataBindingsInstaller), menuName = "Installers/" + nameof(PersistentDataBindingsInstaller))]
public class PersistentDataBindingsInstaller : ScriptableObjectInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<SaveFirstLaunchPersistentDataCommand>().AsSingle();
		Container.Bind<UserIdProxy>().AsSingle();
		Container.Bind<FirstLaunchTimestampProxy>().AsSingle();
	}
}

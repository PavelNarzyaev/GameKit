using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(MainInstaller), menuName = "Installers/" + nameof(MainInstaller))]
public class MainInstaller : ScriptableObjectInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<LaunchCommand>().AsSingle();
		Container.Bind<CurrentTimeProxy>().AsSingle();
	}
}

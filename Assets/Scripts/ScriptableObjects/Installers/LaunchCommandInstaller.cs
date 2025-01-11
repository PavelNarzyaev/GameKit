using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(LaunchCommandInstaller), menuName = "Installers/" + nameof(LaunchCommandInstaller))]
public class LaunchCommandInstaller : ScriptableObjectInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<LaunchCommand>().AsSingle();
	}
}

using TMPro;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _temporaryUserIdText;

	[Inject] private LaunchCommand _launchCommand;
	[Inject] private UserIdProxy _userIdProxy;

	private void Start()
	{
		_launchCommand.Execute();
		_temporaryUserIdText.text = _userIdProxy.Get();
	}
}

using UnityEngine;
using Zenject;

public class NonLazyBindTest
{
	[Inject]
	private void Inject()
	{
		Debug.Log($"Inject {nameof(NonLazyBindTest)}");
	}
}

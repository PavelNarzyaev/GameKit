using UnityEngine;
using Zenject;

public class LazyBindTest
{
	[Inject]
	private void Inject()
	{
		Debug.Log($"Inject {nameof(LazyBindTest)}");
	}
}

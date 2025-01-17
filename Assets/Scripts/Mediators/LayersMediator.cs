using System;
using UnityEngine;

public class LayersMediator
{
	public event Action<Type, LayerNames.Layer> showScreenEvent;
	public event Action<Type> destroyScreenIfExistsEvent;

	public void ShowScreen<T>(LayerNames.Layer layer) where T : MonoBehaviour
	{
		showScreenEvent?.Invoke(typeof(T), layer);
	}

	public void DestroyScreenIfExists<T>() where T : MonoBehaviour
	{
		DestroyScreenIfExists(typeof(T));
	}

	public void DestroyScreenIfExists(Type screenType)
	{
		destroyScreenIfExistsEvent?.Invoke(screenType);
	}
}

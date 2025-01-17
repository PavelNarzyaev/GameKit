using System;
using UnityEngine;

public class LayersMediator
{
	public event Action<Type, LayerNames.Layer> showScreenEvent;

	public void ShowScreen<T>(LayerNames.Layer layer) where T : MonoBehaviour
	{
		showScreenEvent?.Invoke(typeof(T), layer);
	}
}

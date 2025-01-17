using System;

public class LayersMediator
{
	public event Action<Type, LayerNames.Layer> showScreenEvent;
	public event Action<Type> destroyScreenIfExistsEvent;

	public void ShowScreen(Type screenType, LayerNames.Layer layer)
	{
		showScreenEvent?.Invoke(screenType, layer);
	}

	public void DestroyScreenIfExists(Type screenType)
	{
		destroyScreenIfExistsEvent?.Invoke(screenType);
	}
}

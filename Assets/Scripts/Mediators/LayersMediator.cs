using System;

public class LayersMediator
{
	public event Action<Type, Layer> showScreenEvent;
	public event Action<Type> destroyScreenIfExistsEvent;
	public event Action destroyAllScreensEvent;

	public void ShowScreen(Type screenType, Layer layer)
	{
		showScreenEvent?.Invoke(screenType, layer);
	}

	public void DestroyScreenIfExists(Type screenType)
	{
		destroyScreenIfExistsEvent?.Invoke(screenType);
	}

	public void DestroyAllScreens()
	{
		destroyAllScreensEvent?.Invoke();
	}
}

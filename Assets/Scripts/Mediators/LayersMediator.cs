using System;

namespace GameKit
{

	public class LayersMediator
	{
		public event Action<Type, Layer> showScreenEvent;
		public event Action<Type> hideScreenIfExistsEvent;
		public event Action destroyAllScreensEvent;

		public void ShowScreen(Type screenType, Layer layer)
		{
			showScreenEvent?.Invoke(screenType, layer);
		}

		public void HideScreenIfExists(Type screenType)
		{
			hideScreenIfExistsEvent?.Invoke(screenType);
		}

		public void DestroyAllScreens()
		{
			destroyAllScreensEvent?.Invoke();
		}
	}
}

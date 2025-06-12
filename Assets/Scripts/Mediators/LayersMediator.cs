using System;
using Data.Constants;
using JetBrains.Annotations;

namespace Mediators
{
    [UsedImplicitly]
    public class LayersMediator
    {
        public event Action<Type, Layer> ShowScreenEvent;
        public event Action<Type> HideScreenIfExistsEvent;
        public event Action DestroyAllScreensEvent;

        public void ShowScreen(Type screenType, Layer layer)
        {
            ShowScreenEvent?.Invoke(screenType, layer);
        }

        public void HideScreenIfExists(Type screenType)
        {
            HideScreenIfExistsEvent?.Invoke(screenType);
        }

        public void DestroyAllScreens()
        {
            DestroyAllScreensEvent?.Invoke();
        }
    }
}

using System;
using JetBrains.Annotations;
using Data.Constants;

namespace Mediators
{
    [UsedImplicitly]
    public class LayersMediator
    {
        public event Action<Type, ELayer> showScreenEvent;
        public event Action<Type> hideScreenIfExistsEvent;
        public event Action destroyAllScreensEvent;

        public void ShowScreen(Type screenType, ELayer layer) => showScreenEvent?.Invoke(screenType, layer);
        public void HideScreenIfExists(Type screenType) => hideScreenIfExistsEvent?.Invoke(screenType);
        public void DestroyAllScreens() => destroyAllScreensEvent?.Invoke();
    }
}

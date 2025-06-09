using System;
using Data.Constants;
using Zenject;

namespace Mediators
{
    public class PagesLayerMediator
    {
        [Inject] private LayersMediator _layersMediator;

        public event Action changePageEvent;
        public Type CurrentPageType { get; private set; }

        public void ShowPage(Type pageScreenType)
        {
            if (CurrentPageType != null)
                _layersMediator.HideScreenIfExists(CurrentPageType);
            _layersMediator.ShowScreen(pageScreenType, Layer.Page);
            CurrentPageType = pageScreenType;
            changePageEvent?.Invoke();
        }
    }
}

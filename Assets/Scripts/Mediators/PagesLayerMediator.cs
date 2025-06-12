using System;
using Data.Constants;
using JetBrains.Annotations;
using Zenject;

namespace Mediators
{
    [UsedImplicitly]
    public class PagesLayerMediator
    {
        [Inject] private LayersMediator _layersMediator;
        public Type CurrentPageType { get; private set; }

        public event Action changePageEvent;

        public void ShowPage(Type pageScreenType)
        {
            if (CurrentPageType != null)
            {
                _layersMediator.HideScreenIfExists(CurrentPageType);
            }

            _layersMediator.ShowScreen(pageScreenType, Layer.Page);
            CurrentPageType = pageScreenType;
            changePageEvent?.Invoke();
        }
    }
}

using System;
using Data.Constants;
using JetBrains.Annotations;
using Zenject;

namespace Mediators
{
    [UsedImplicitly]
    public class PagesLayerMediator
    {
        [Inject] private LayersMediator m_layersMediator;
        public Type CurrentPageType { get; private set; }

        public event Action ChangePageEvent;

        public void ShowPage(Type pageScreenType)
        {
            if (CurrentPageType != null)
            {
                m_layersMediator.HideScreenIfExists(CurrentPageType);
            }

            m_layersMediator.ShowScreen(pageScreenType, Layer.Page);
            CurrentPageType = pageScreenType;
            ChangePageEvent?.Invoke();
        }
    }
}

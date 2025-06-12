using Data.Constants;
using JetBrains.Annotations;
using MonoBehaviours.Screens;
using Zenject;

namespace Mediators
{
    [UsedImplicitly]
    public class PopupsLayerMediator
    {
        [Inject] private LayersMediator m_layersMediator;

        public void ShowNavigationPopup()
        {
            m_layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
            m_layersMediator.ShowScreen(typeof(NavigationPopupScreen), Layer.Popups);
        }

        public void CloseNavigationPopup()
        {
            m_layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
            m_layersMediator.HideScreenIfExists(typeof(NavigationPopupScreen));
        }
    }
}

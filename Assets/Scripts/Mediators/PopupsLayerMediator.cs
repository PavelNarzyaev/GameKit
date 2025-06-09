using Data.Constants;
using MonoBehaviours.Screens;
using Zenject;

namespace Mediators
{
    public class PopupsLayerMediator
    {
        [Inject] private LayersMediator _layersMediator;

        public void ShowNavigationPopup()
        {
            _layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
            _layersMediator.ShowScreen(typeof(NavigationPopupScreen), Layer.Popups);
        }

        public void CloseNavigationPopup()
        {
            _layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
            _layersMediator.HideScreenIfExists(typeof(NavigationPopupScreen));
        }
    }
}

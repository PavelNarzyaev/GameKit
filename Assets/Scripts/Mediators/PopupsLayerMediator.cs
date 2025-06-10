using Data.Constants;
using JetBrains.Annotations;
using MonoBehaviours.Screens;
using Zenject;

namespace Mediators
{
    [UsedImplicitly]
    public class PopupsLayerMediator
    {
        [Inject] private LayersMediator _layersMediator;

        public void ShowNavigationPopup()
        {
            _layersMediator.ShowScreen(typeof(PopupShadeScreen), ELayer.Popups);
            _layersMediator.ShowScreen(typeof(NavigationPopupScreen), ELayer.Popups);
        }

        public void CloseNavigationPopup()
        {
            _layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
            _layersMediator.HideScreenIfExists(typeof(NavigationPopupScreen));
        }
    }
}

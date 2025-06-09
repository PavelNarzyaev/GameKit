using Data.Constants;
using JetBrains.Annotations;
using Mediators;
using MonoBehaviours.Screens;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class ResetUiCommand
    {
        [Inject] private LayersMediator _layersMediator;
        [Inject] private PagesLayerMediator _pagesLayerMediator;

        public void Execute()
        {
            _layersMediator.DestroyAllScreens();

            _pagesLayerMediator.ShowPage(typeof(MainPageScreen));
            _layersMediator.ShowScreen(typeof(NavigationPanelScreen), Layer.NavigationPanel);
            _layersMediator.ShowScreen(typeof(DesignOverlayScreen), Layer.DesignOverlay);
        }
    }
}

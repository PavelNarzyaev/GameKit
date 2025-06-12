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
        [Inject] private LayersMediator m_layersMediator;
        [Inject] private PagesLayerMediator m_pagesLayerMediator;

        public void Execute()
        {
            m_layersMediator.DestroyAllScreens();

            m_pagesLayerMediator.ShowPage(typeof(MainPageScreen));
            m_layersMediator.ShowScreen(typeof(NavigationPanelScreen), Layer.NavigationPanel);
            m_layersMediator.ShowScreen(typeof(DesignOverlayScreen), Layer.DesignOverlay);
        }
    }
}

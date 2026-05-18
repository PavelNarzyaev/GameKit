using GameKit.UiDebugPanel.Contracts;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.UiDebugPanel
{
    [UsedImplicitly]
    public class DebugPanelMessageNavigator : IDebugPanelMessageNavigator
    {
        [Inject] private IUiRegionHostPresenter m_uiRegionHostPresenter;

        public void ShowMessage(string addressableId)
        {
            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(addressableId);
            m_uiRegionHostPresenter.OnRegionElementShowing(addressableId, UiRegionId.DebugPanelMessage);
        }

        public void HideMessage(string addressableId)
        {
            m_uiRegionHostPresenter.OnRegionElementHidingIfExists(addressableId);
        }
    }
}

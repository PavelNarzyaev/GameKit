using GameKit.UiRegions;
using Zenject;

namespace GameKit.LogsDebugPage
{
    public class LogsDebugPageView : UiRegionElement
    {
        [Inject] private LogsDebugPagePresenter m_presenter;
    }
}

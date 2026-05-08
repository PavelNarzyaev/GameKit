using GameKit.UiDebugShared;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.StateDebugPage
{
    public class StateDebugPageView : UiRegionElement
    {
        [SerializeField] private DebugValue userId;
        [SerializeField] private DebugValue firstLaunchTime;
        [SerializeField] private DebugValue launchCount;
        [SerializeField] private DebugButton copyToClipboardButton;
        [SerializeField] private DebugButton pasteFromClipboardButton;
        [SerializeField] private DebugButton resetButton;

        [Inject] private StateDebugPagePresenter m_presenter;

        private void Awake()
        {
            copyToClipboardButton.AddClickListener(m_presenter.CopyStateToClipboard);
            pasteFromClipboardButton.AddClickListener(m_presenter.PasteStateFromClipboard);
            resetButton.AddClickListener(m_presenter.ResetState);
        }

        private void Start()
        {
            userId.SetValueText(m_presenter.GetUserIdText());
            firstLaunchTime.SetValueText(m_presenter.GetFirstLaunchTimeText());
            launchCount.SetValueText(m_presenter.GetLaunchCountText());
        }
    }
}

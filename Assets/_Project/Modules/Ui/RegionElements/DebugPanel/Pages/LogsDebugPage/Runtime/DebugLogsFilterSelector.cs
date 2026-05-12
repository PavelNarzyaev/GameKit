using GameKit.UiDebugShared;
using UnityEngine;
using Zenject;

namespace GameKit.LogsDebugPage
{
    public class DebugLogsFilterSelector : MonoBehaviour
    {
        [SerializeField] private DebugButton allTab;
        [SerializeField] private DebugButton problemsTab;
        [Inject] private DebugLogsFilterSelectorPresenter m_presenter;

        private void Awake()
        {
            allTab.AddClickListener(m_presenter.ShowAll);
            problemsTab.AddClickListener(m_presenter.ShowProblems);
        }

        private void OnEnable()
        {
            Refresh();
            m_presenter.Changed += HandleChanged;
        }

        private void OnDisable()
        {
            m_presenter.Changed -= HandleChanged;
        }

        private void HandleChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            allTab.SetEnabled(m_presenter.CurrentFilter != LogsDebugPageFilter.All);
            problemsTab.SetEnabled(m_presenter.CurrentFilter != LogsDebugPageFilter.Problems);
        }
    }
}

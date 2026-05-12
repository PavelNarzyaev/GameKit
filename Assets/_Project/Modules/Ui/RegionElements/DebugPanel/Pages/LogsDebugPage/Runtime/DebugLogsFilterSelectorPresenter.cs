using System;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.LogsDebugPage
{
    [UsedImplicitly]
    public class DebugLogsFilterSelectorPresenter : IDisposable
    {
        [Inject] private LogsDebugPagePresenter m_logsDebugPagePresenter;

        public LogsDebugPageFilter CurrentFilter => m_logsDebugPagePresenter.CurrentFilter;

        public event Action Changed;

        [Inject]
        private void Inject()
        {
            m_logsDebugPagePresenter.FilterChanged += HandleFilterChanged;
        }

        public void Dispose()
        {
            m_logsDebugPagePresenter.FilterChanged -= HandleFilterChanged;
        }

        public void ShowAll()
        {
            m_logsDebugPagePresenter.SetFilter(LogsDebugPageFilter.All);
        }

        public void ShowProblems()
        {
            m_logsDebugPagePresenter.SetFilter(LogsDebugPageFilter.Problems);
        }

        private void HandleFilterChanged()
        {
            Changed?.Invoke();
        }
    }
}

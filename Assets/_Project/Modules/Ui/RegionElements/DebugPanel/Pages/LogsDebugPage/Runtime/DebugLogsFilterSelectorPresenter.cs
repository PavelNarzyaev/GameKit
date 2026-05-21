using System;
using JetBrains.Annotations;

namespace GameKit.LogsDebugPage
{
    [UsedImplicitly]
    public class DebugLogsFilterSelectorPresenter : IDisposable
    {
        private readonly LogsDebugPagePresenter m_logsDebugPagePresenter;

        public LogsDebugPageFilter CurrentFilter => m_logsDebugPagePresenter.CurrentFilter;

        public event Action Changed;

        public DebugLogsFilterSelectorPresenter(LogsDebugPagePresenter logsDebugPagePresenter)
        {
            m_logsDebugPagePresenter = logsDebugPagePresenter;
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

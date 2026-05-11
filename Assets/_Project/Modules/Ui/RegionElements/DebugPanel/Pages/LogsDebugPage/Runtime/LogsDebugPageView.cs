using GameKit.UiRegions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.LogsDebugPage
{
    public class LogsDebugPageView : UiRegionElement
    {
        [SerializeField] private TMP_Text logsText;
        [SerializeField] private Button allButton;
        [SerializeField] private Button problemsButton;
        [SerializeField] private Button copyButton;
        [Inject] private LogsDebugPagePresenter m_presenter;

        private void Awake()
        {
            allButton.onClick.AddListener(m_presenter.ShowAll);
            problemsButton.onClick.AddListener(m_presenter.ShowProblems);
            copyButton.onClick.AddListener(m_presenter.CopyAllLogs);
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
            logsText.text = m_presenter.GetLogsText();
        }
    }
}

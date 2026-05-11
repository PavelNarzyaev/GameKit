using System.Collections;
using GameKit.UiRegions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.LogsDebugPage
{
    public class LogsDebugPageView : UiRegionElement
    {
        [SerializeField] private ScrollRect logsScrollRect;
        [SerializeField] private CanvasGroup logsContainerCanvasGroup;
        [SerializeField] private TMP_Text logsText;
        [SerializeField] private Button allButton;
        [SerializeField] private Button problemsButton;
        [SerializeField] private Button copyButton;
        [Inject] private LogsDebugPagePresenter m_presenter;
        private Coroutine m_scrollCoroutine;

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

            if (m_scrollCoroutine == null)
            {
                return;
            }

            StopCoroutine(m_scrollCoroutine);
            m_scrollCoroutine = null;
        }

        private void HandleChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            logsContainerCanvasGroup.alpha = 0f;
            logsText.text = m_presenter.GetLogsText();
            ScrollLogsToBottom();
        }

        private void ScrollLogsToBottom()
        {
            if (m_scrollCoroutine != null)
            {
                return;
            }

            m_scrollCoroutine = StartCoroutine(ScrollLogsToBottomNextFrame());
        }

        private IEnumerator ScrollLogsToBottomNextFrame()
        {
            yield return null;

            logsScrollRect.StopMovement();
            logsScrollRect.verticalNormalizedPosition = 0f;

            logsContainerCanvasGroup.alpha = 1f;
            m_scrollCoroutine = null;
        }
    }
}

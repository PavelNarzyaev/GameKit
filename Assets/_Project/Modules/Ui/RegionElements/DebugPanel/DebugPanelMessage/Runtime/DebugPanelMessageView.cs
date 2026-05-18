using System.Collections;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.DebugPanelMessage
{
    public class DebugPanelMessageView : UiRegionElement
    {
        private const float k_DurationSeconds = 2f;

        [Inject] private DebugPanelMessagePresenter m_presenter;
        private Coroutine m_hideCoroutine;

        private void OnEnable()
        {
            m_hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private void OnDisable()
        {
            if (m_hideCoroutine != null)
            {
                StopCoroutine(m_hideCoroutine);
                m_hideCoroutine = null;
            }
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(k_DurationSeconds);

            m_hideCoroutine = null;
            m_presenter.Hide(AddressableId);
        }
    }
}

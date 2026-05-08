using GameKit.UiRegions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.TopPanel
{
    public class TopPanelView : UiRegionElement
    {
        [SerializeField] private Button settingsButton;

        [Inject] private TopPanelPresenter m_presenter;

        private void Awake()
        {
            settingsButton.onClick.AddListener(m_presenter.OpenSettingsPopup);
        }
    }
}

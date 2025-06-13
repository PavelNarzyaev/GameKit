using System;
using Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class NavigationPopupScreen : ScreenAbstract
    {
        [SerializeField] private Button mainPageButton;
        [SerializeField] private Button statePageButton;
        [SerializeField] private Button timePageButton;
        [SerializeField] private Button closeButton;

        [Inject] private PagesLayerMediator m_pagesLayerMediator;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void Awake()
        {
            SetUpButton(mainPageButton, typeof(MainPageScreen));
            SetUpButton(statePageButton, typeof(StatePageScreen));
            SetUpButton(timePageButton, typeof(TimePageScreen));

            closeButton.onClick.AddListener(m_popupsLayerMediator.CloseNavigationPopup);
        }

        private void SetUpButton(Button button, Type pageType)
        {
            button.onClick.AddListener(() => m_pagesLayerMediator.ShowPage(pageType));
        }
    }
}

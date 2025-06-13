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

        [Inject] private PagesLayerMediator m_pagesLayerMediator;

        private void Awake()
        {
            SetUpButton(mainPageButton, typeof(MainPageScreen));
            SetUpButton(statePageButton, typeof(StatePageScreen));
            SetUpButton(timePageButton, typeof(TimePageScreen));
        }

        private void SetUpButton(Button button, Type pageType)
        {
            button.onClick.AddListener(() => m_pagesLayerMediator.ShowPage(pageType));
        }
    }
}

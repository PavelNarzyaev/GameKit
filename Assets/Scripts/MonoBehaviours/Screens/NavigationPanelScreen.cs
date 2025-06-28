using System;
using System.Collections.Generic;
using Mediators;
using UnityEngine;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class NavigationPanelScreen : ScreenAbstract
    {
        [SerializeField] private NavigationPanelToggleButton mainPageButton;
        [SerializeField] private NavigationPanelToggleButton statePageButton;
        [SerializeField] private NavigationPanelToggleButton timePageButton;
        [SerializeField] private NavigationPanelToggleButton currenciesPageButton;

        private readonly Dictionary<Type, NavigationPanelToggleButton> m_buttonByType = new();

        [Inject] private PagesLayerMediator m_pagesLayerMediator;
        private NavigationPanelToggleButton m_selectedButton;

        private void Awake()
        {
            SetUpButton(mainPageButton, typeof(MainPageScreen));
            SetUpButton(statePageButton, typeof(StatePageScreen));
            SetUpButton(timePageButton, typeof(TimePageScreen));
            SetUpButton(currenciesPageButton, typeof(CurrenciesPageScreen));
        }

        private void Start()
        {
            if (m_pagesLayerMediator.CurrentPageType != null)
            {
                Refresh();
            }
        }

        private void OnEnable()
        {
            m_pagesLayerMediator.ChangedPageEvent += Refresh;
        }

        private void OnDisable()
        {
            m_pagesLayerMediator.ChangedPageEvent -= Refresh;
        }

        private void SetUpButton(NavigationPanelToggleButton button, Type pageType)
        {
            button.SetSelected(m_pagesLayerMediator.CurrentPageType == pageType);
            m_buttonByType.Add(pageType, button);
            button.AddClickListener(() => m_pagesLayerMediator.ShowPage(pageType));
        }

        private void Refresh()
        {
            if (m_selectedButton)
            {
                m_selectedButton.SetSelected(false);
            }

            m_selectedButton = m_buttonByType[m_pagesLayerMediator.CurrentPageType];
            m_selectedButton.SetSelected(true);
        }
    }
}

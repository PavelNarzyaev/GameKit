using System;
using System.Collections.Generic;
using Mediators;
using UnityEngine;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class NavigationPanelScreen : ScreenAbstract
    {
        [SerializeField] private NavigationPanelToggleButton _mainPageButton;
        [SerializeField] private NavigationPanelToggleButton _statePageButton;
        [SerializeField] private NavigationPanelToggleButton _timePageButton;

        [Inject] private PagesLayerMediator _pagesLayerMediator;

        private Dictionary<Type, NavigationPanelToggleButton> _buttonByType = new();
        private NavigationPanelToggleButton _selectedButton;

        private void Awake()
        {
            SetUpButton(_mainPageButton, typeof(MainPageScreen));
            SetUpButton(_statePageButton, typeof(StatePageScreen));
            SetUpButton(_timePageButton, typeof(TimePageScreen));
        }

        private void SetUpButton(NavigationPanelToggleButton button, Type pageType)
        {
            button.SetSelected(_pagesLayerMediator.CurrentPageType == pageType);
            _buttonByType.Add(pageType, button);
            button.AddClickListener(() => _pagesLayerMediator.ShowPage(pageType));
        }

        private void Start()
        {
            if (_pagesLayerMediator.CurrentPageType != null)
                Refresh();
        }

        private void OnEnable()
        {
            _pagesLayerMediator.changePageEvent += Refresh;
        }

        private void OnDisable()
        {
            _pagesLayerMediator.changePageEvent -= Refresh;
        }

        private void Refresh()
        {
            if (_selectedButton != null)
                _selectedButton.SetSelected(false);
            _selectedButton = _buttonByType[_pagesLayerMediator.CurrentPageType];
            _selectedButton.SetSelected(true);
        }
    }
}

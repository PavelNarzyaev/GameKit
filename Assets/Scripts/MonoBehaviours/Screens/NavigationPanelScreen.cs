using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NavigationPanelScreen : MonoBehaviour
{
	[SerializeField] private NavigationPanelToggleButton _mainPageButton;
	[SerializeField] private NavigationPanelToggleButton _analyticsPageButton;

	[Inject] private PagesLayerMediator _pagesLayerMediator;

	private Dictionary<Type, NavigationPanelToggleButton> _buttonByType = new();
	private NavigationPanelToggleButton _selectedButton;

	private void Awake()
	{
		SetUpButton(_mainPageButton, typeof(MainPageScreen));
		SetUpButton(_analyticsPageButton, typeof(StatePageScreen));
	}

	private void SetUpButton(NavigationPanelToggleButton button, Type pageType)
	{
		button.SetSelected(_pagesLayerMediator.currentPageType == pageType);
		_buttonByType.Add(pageType, button);
		button.AddClickListener(() => _pagesLayerMediator.ShowPage(pageType));
	}

	private void Start()
	{
		if (_pagesLayerMediator.currentPageType != null)
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
		_selectedButton = _buttonByType[_pagesLayerMediator.currentPageType];
		_selectedButton.SetSelected(true);
	}
}

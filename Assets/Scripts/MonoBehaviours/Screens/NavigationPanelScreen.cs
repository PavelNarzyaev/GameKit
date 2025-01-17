using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NavigationPanelScreen : MonoBehaviour
{
	[SerializeField] private Button _mainPageButton;
	[SerializeField] private Button _analyticsPageButton;

	[Inject] private PagesLayerMediator _pagesLayerMediator;

	private void Awake()
	{
		_mainPageButton.onClick.AddListener(() => _pagesLayerMediator.ShowPage<MainPageScreen>());
		_analyticsPageButton.onClick.AddListener(() => _pagesLayerMediator.ShowPage<AnalyticsPageScreen>());
	}
}

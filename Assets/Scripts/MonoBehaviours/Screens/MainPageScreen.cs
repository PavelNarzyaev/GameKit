using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainPageScreen : ScreenAbstract
{
	[SerializeField] private Button _showNavigationPopupButton;
	[Inject] private PopupsLayerMediator _popupsLayerMediator;

	private void Awake()
	{
		_showNavigationPopupButton.onClick.AddListener(_popupsLayerMediator.ShowNavigationPopup);
	}
}

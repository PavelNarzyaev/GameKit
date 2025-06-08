using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit
{

	public class PopupShadeScreen : ScreenAbstract
	{
		[SerializeField] private Button _button;
		[Inject] private PopupsLayerMediator _popupsLayerMediator;

		private void OnEnable()
		{
			_button.onClick.AddListener(_popupsLayerMediator.CloseNavigationPopup);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveAllListeners();
		}

		public override bool IsCached()
		{
			return true;
		}
	}
}

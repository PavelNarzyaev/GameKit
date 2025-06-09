using Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class MainPageScreen : ScreenAbstract
    {
        [SerializeField] private Button _showNavigationPopupButton;
        [Inject] private PopupsLayerMediator _popupsLayerMediator;

        private void Awake()
        {
            _showNavigationPopupButton.onClick.AddListener(_popupsLayerMediator.ShowNavigationPopup);
        }
    }
}

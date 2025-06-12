using Mediators;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class MainPageScreen : ScreenAbstract
    {
        [FormerlySerializedAs("_showNavigationPopupButton")] [SerializeField] private Button showNavigationPopupButton;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void Awake()
        {
            showNavigationPopupButton.onClick.AddListener(m_popupsLayerMediator.ShowNavigationPopup);
        }
    }
}

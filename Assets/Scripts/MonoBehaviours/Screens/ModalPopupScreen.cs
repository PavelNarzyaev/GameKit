using Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class ModalPopupScreen : ScreenAbstract
    {
        [SerializeField] private Button closeButton;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void Awake()
        {
            closeButton.onClick.AddListener(m_popupsLayerMediator.Close<ModalPopupScreen>);
        }
    }
}

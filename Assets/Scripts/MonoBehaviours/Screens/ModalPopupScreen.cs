using Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class ModalPopupScreen : PopupScreenBase
    {
        [SerializeField] private Button closeButton;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void Awake()
        {
            closeButton.onClick.AddListener(m_popupsLayerMediator.Close<ModalPopupScreen>);
        }

        protected override bool IsModal()
        {
            return true;
        }
    }
}

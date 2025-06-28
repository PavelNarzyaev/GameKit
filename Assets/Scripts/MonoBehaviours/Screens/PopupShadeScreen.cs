using Mediators;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class PopupShadeScreen : ScreenAbstract
    {
        [SerializeField] private Button button;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            if (!m_popupsLayerMediator.IsFrontPopupModal)
            {
                m_popupsLayerMediator.CloseFront();
            }
        }

        public override bool IsCached()
        {
            return true;
        }
    }
}

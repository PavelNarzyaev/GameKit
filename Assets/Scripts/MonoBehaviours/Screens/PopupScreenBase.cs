using Mediators;
using UnityEngine;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class PopupScreenBase : ScreenAbstract
    {
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void OnEnable()
        {
            m_popupsLayerMediator.ChangeEvent += ChangeHandler;
        }

        private void OnDisable()
        {
            m_popupsLayerMediator.ChangeEvent -= ChangeHandler;
        }

        private void ChangeHandler()
        {
            if (m_popupsLayerMediator.LastOpenedPopup() != GetType())
            {
                return;
            }

            m_popupsLayerMediator.IsLastPopupModal = IsModal();
        }

        protected virtual bool IsModal()
        {
            return false;
        }
    }
}

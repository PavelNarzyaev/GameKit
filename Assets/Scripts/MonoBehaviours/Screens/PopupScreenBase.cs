using System;
using Mediators;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class PopupScreenBase : ScreenAbstract
    {
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void OnEnable()
        {
            m_popupsLayerMediator.FrontPopupChangedEvent += FrontPopupChangedHandler;
        }

        private void OnDisable()
        {
            m_popupsLayerMediator.FrontPopupChangedEvent -= FrontPopupChangedHandler;
        }

        private void FrontPopupChangedHandler(Type type)
        {
            if (type != GetType())
            {
                return;
            }

            m_popupsLayerMediator.IsFrontPopupModal = IsModal();
        }

        protected virtual bool IsModal()
        {
            return false;
        }
    }
}

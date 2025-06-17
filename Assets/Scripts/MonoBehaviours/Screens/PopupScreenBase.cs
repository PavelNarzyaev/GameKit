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
            m_popupsLayerMediator.TopPopupChangedEvent += TopPopupChangedHandler;
        }

        private void OnDisable()
        {
            m_popupsLayerMediator.TopPopupChangedEvent -= TopPopupChangedHandler;
        }

        private void TopPopupChangedHandler(Type type)
        {
            if (type != GetType())
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

using System;
using System.Collections.Generic;
using System.Linq;
using Data.Constants;
using JetBrains.Annotations;
using MonoBehaviours.Screens;
using Zenject;

namespace Mediators
{
    [UsedImplicitly]
    public class PopupsLayerMediator
    {
        [Inject] private LayersMediator m_layersMediator;
        private readonly List<Type> m_stack = new();
        public bool IsFrontPopupModal;
        public event Action<Type> FrontPopupChangedEvent;

        public void Open<T>() where T : PopupScreenBase
        {
            if (m_stack.Count == 0)
            {
                m_layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
            }

            m_layersMediator.ShowScreen(typeof(T), Layer.Popups);
            FrontPopupChangedEvent?.Invoke(typeof(T));
            m_stack.Add(typeof(T));
            RefreshShadeIndex();
        }

        public void Close<T>() where T : PopupScreenBase
        {
            Close(typeof(T));
        }

        private void Close(Type type)
        {
            m_layersMediator.HideScreenIfExists(type);
            m_stack.Remove(type);

            if (m_stack.Count == 0)
            {
                m_layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
            }
            else
            {
                RefreshShadeIndex();
                FrontPopupChangedEvent?.Invoke(m_stack.Last());
            }
        }

        public void CloseFront()
        {
            Close(m_stack.Last());
        }

        private void RefreshShadeIndex()
        {
            m_layersMediator.SetScreenIndex(typeof(PopupShadeScreen), m_stack.Count - 1);
        }
    }
}

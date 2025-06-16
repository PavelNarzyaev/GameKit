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

        public void Open<T>() where T : ScreenAbstract
        {
            if (m_stack.Count == 0)
            {
                m_layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
            }

            m_layersMediator.ShowScreen(typeof(T), Layer.Popups);
            m_stack.Add(typeof(T));
            RefreshShadeIndex();
        }

        public void Close<T>() where T : ScreenAbstract
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
            }
        }

        public void CloseLastOpened()
        {
            Close(m_stack.Last());
        }

        private void RefreshShadeIndex()
        {
            m_layersMediator.SetScreenIndex(typeof(PopupShadeScreen), m_stack.Count - 1);
        }
    }
}

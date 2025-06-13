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
        private int m_popupsCounter;

        public void Open<T>() where T : ScreenAbstract
        {
            if (m_popupsCounter == 0)
            {
                m_layersMediator.ShowScreen(typeof(PopupShadeScreen), Layer.Popups);
            }

            m_layersMediator.ShowScreen(typeof(T), Layer.Popups);
            m_popupsCounter++;
        }

        public void Close<T>() where T : ScreenAbstract
        {
            m_layersMediator.HideScreenIfExists(typeof(T));
            m_popupsCounter--;

            if (m_popupsCounter == 0)
            {
                m_layersMediator.HideScreenIfExists(typeof(PopupShadeScreen));
            }
        }
    }
}

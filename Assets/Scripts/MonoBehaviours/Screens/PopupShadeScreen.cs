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
            button.onClick.AddListener(m_popupsLayerMediator.Close<NavigationPopupScreen>);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        public override bool IsCached()
        {
            return true;
        }
    }
}

using Mediators;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class PopupShadeScreen : ScreenAbstract
    {
        [FormerlySerializedAs("_button")] [SerializeField] private Button button;
        [Inject] private PopupsLayerMediator m_popupsLayerMediator;

        private void OnEnable()
        {
            button.onClick.AddListener(m_popupsLayerMediator.CloseNavigationPopup);
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

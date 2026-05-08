using GameKit.UiRegions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.CorePage
{
    public class CorePageView : UiRegionElement
    {
        [SerializeField] private Button metaPageButton;

        [Inject] private CorePagePresenter m_presenter;

        private void Awake()
        {
            metaPageButton.onClick.AddListener(() => m_presenter.OpenMetaPage());
        }
    }
}

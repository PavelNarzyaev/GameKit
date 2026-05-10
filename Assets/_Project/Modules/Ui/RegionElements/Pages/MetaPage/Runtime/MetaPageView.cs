using GameKit.UiRegions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameKit.MetaPage
{
    public class MetaPageView : UiRegionElement
    {
        [SerializeField] private Button corePageButton;

        [Inject] private MetaPagePresenter m_presenter;

        private void Awake()
        {
            corePageButton.onClick.AddListener(() => m_presenter.OpenCorePage());
        }
    }
}

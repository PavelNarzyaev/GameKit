using GameKit.UiDebugShared;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.CurrenciesDebugPage
{
    public class CurrenciesDebugPageView : UiRegionElement
    {
        [SerializeField] private DebugValue coins;
        [SerializeField] private DebugButton remove10CoinsButton;
        [SerializeField] private DebugButton remove1CoinButton;
        [SerializeField] private DebugButton add1CoinButton;
        [SerializeField] private DebugButton add10CoinsButton;
        [SerializeField] private DebugValue diamonds;
        [SerializeField] private DebugButton remove10DiamondsButton;
        [SerializeField] private DebugButton remove1DiamondButton;
        [SerializeField] private DebugButton add1DiamondButton;
        [SerializeField] private DebugButton add10DiamondsButton;

        [Inject] private CurrenciesDebugPagePresenter m_presenter;

        private void Awake()
        {
            remove10CoinsButton.AddClickListener(() => m_presenter.SpendSoft(10));
            remove1CoinButton.AddClickListener(() => m_presenter.SpendSoft(1));
            add1CoinButton.AddClickListener(() => m_presenter.AddSoft(1));
            add10CoinsButton.AddClickListener(() => m_presenter.AddSoft(10));

            remove10DiamondsButton.AddClickListener(() => m_presenter.SpendHard(10));
            remove1DiamondButton.AddClickListener(() => m_presenter.SpendHard(1));
            add1DiamondButton.AddClickListener(() => m_presenter.AddHard(1));
            add10DiamondsButton.AddClickListener(() => m_presenter.AddHard(10));
        }

        private void OnEnable()
        {
            Refresh();
            m_presenter.Changed += HandleChanged;
        }

        private void OnDisable()
        {
            m_presenter.Changed -= HandleChanged;
        }

        private void HandleChanged()
        {
            Refresh();
        }

        private void Refresh()
        {
            coins.SetValueText(m_presenter.GetSoftText());
            diamonds.SetValueText(m_presenter.GetHardText());
        }
    }
}

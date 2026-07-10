using System;
using System.Globalization;
using GameKit.UiDebugShared;
using GameKit.UiRegions;
using R3;
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
        private IDisposable m_softCurrencySubscription;
        private IDisposable m_hardCurrencySubscription;

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
            m_softCurrencySubscription = m_presenter.SoftCurrency.Subscribe(RefreshSoftCurrency);
            m_hardCurrencySubscription = m_presenter.HardCurrency.Subscribe(RefreshHardCurrency);
        }

        private void OnDisable()
        {
            m_softCurrencySubscription.Dispose();
            m_hardCurrencySubscription.Dispose();
        }

        private void RefreshSoftCurrency(int value)
        {
            coins.SetValueText(value.ToString(CultureInfo.InvariantCulture));
        }

        private void RefreshHardCurrency(int value)
        {
            diamonds.SetValueText(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}

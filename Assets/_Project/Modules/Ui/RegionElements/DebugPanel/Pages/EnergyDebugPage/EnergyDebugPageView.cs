using GameKit.UiDebugShared;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;

namespace GameKit.EnergyDebugPage
{
    public class EnergyDebugPageView : UiRegionElement
    {
        [SerializeField] private DebugValue energy;
        [SerializeField] private DebugButton remove10EnergyButton;
        [SerializeField] private DebugButton remove1EnergyButton;
        [SerializeField] private DebugButton add1EnergyButton;
        [SerializeField] private DebugButton add10EnergyButton;
        [SerializeField] private DebugValue oneEnergyRestorationSeconds;
        [SerializeField] private DebugValue restorationLimit;
        [SerializeField] private DebugValue restorationTimer;
        [Inject] private EnergyDebugPagePresenter m_presenter;

        private void Awake()
        {
            remove10EnergyButton.AddClickListener(() => m_presenter.TrySpend(10));
            remove1EnergyButton.AddClickListener(() => m_presenter.TrySpend(1));
            add1EnergyButton.AddClickListener(() => m_presenter.TryAdd(1));
            add10EnergyButton.AddClickListener(() => m_presenter.TryAdd(10));
        }

        private void OnEnable()
        {
            RefreshOneEnergyRestorationSeconds();
            RefreshRestorationLimit();
            RefreshCurrentEnergy();
            RefreshRestorationTimer();

            m_presenter.Changed += HandleChanged;
        }

        private void Update()
        {
            RefreshRestorationTimer();
        }

        private void OnDisable()
        {
            m_presenter.Changed -= HandleChanged;
        }

        private void HandleChanged()
        {
            RefreshCurrentEnergy();
        }

        private void RefreshOneEnergyRestorationSeconds()
        {
            oneEnergyRestorationSeconds.SetValueText(m_presenter.GetOneEnergyRestorationSecondsText());
        }

        private void RefreshRestorationLimit()
        {
            restorationLimit.SetValueText(m_presenter.GetRestorationLimitText());
        }

        private void RefreshRestorationTimer()
        {
            restorationTimer.SetValueText(m_presenter.GetRestorationTimerText());
        }

        private void RefreshCurrentEnergy()
        {
            energy.SetValueText(m_presenter.GetEnergyText());
        }
    }
}

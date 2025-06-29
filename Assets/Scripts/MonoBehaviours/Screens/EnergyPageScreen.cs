using Proxies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class EnergyPageScreen : ScreenAbstract
    {
        [SerializeField] private DebugValue energy;
        [SerializeField] private Button remove10EnergyButton;
        [SerializeField] private Button remove1EnergyButton;
        [SerializeField] private Button add1EnergyButton;
        [SerializeField] private Button add10EnergyButton;
        [SerializeField] private DebugValue lastRestorationDatetime;
        [SerializeField] private DebugValue nextRestorationTimer;
        [Inject] private EnergyProxy m_energyProxy;

        private void Awake()
        {
            remove10EnergyButton.onClick.AddListener(() => m_energyProxy.TryToSpend(10));
            remove1EnergyButton.onClick.AddListener(() => m_energyProxy.TryToSpend(1));
            add1EnergyButton.onClick.AddListener(() => m_energyProxy.Add(1));
            add10EnergyButton.onClick.AddListener(() => m_energyProxy.Add(10));
        }

        private void OnEnable()
        {
            RefreshLastRestorationDatetime();
            RefreshNextRestorationTimer();
            RefreshCurrentEnergy();

            m_energyProxy.ChangedEvent += RefreshCurrentEnergy;
        }

        private void OnDisable()
        {
            m_energyProxy.ChangedEvent -= RefreshCurrentEnergy;
        }

        private void RefreshLastRestorationDatetime()
        {
            lastRestorationDatetime.SetValueText("TODO");
        }

        private void RefreshNextRestorationTimer()
        {
            nextRestorationTimer.SetValueText("TODO");
        }

        private void RefreshCurrentEnergy()
        {
            energy.SetValueText(m_energyProxy.Energy.ToString());
        }
    }
}

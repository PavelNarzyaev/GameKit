using Proxies;
using ScriptableObjects.Configs;
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
        [SerializeField] private DebugValue oneEnergyRestorationSeconds;
        [SerializeField] private DebugValue restorationLimit;
        [SerializeField] private DebugValue restorationTimer;
        [Inject] private EnergyProxy m_energyProxy;
        [Inject] private MainConfig m_mainConfig;

        private void Awake()
        {
            remove10EnergyButton.onClick.AddListener(() => m_energyProxy.TryToSpend(10));
            remove1EnergyButton.onClick.AddListener(() => m_energyProxy.TryToSpend(1));
            add1EnergyButton.onClick.AddListener(() => m_energyProxy.Add(1));
            add10EnergyButton.onClick.AddListener(() => m_energyProxy.Add(10));
        }

        private void OnEnable()
        {
            RefreshOneEnergyRestorationSeconds();
            RefreshRestorationLimit();
            RefreshCurrentEnergy();

            m_energyProxy.ChangedEvent += RefreshCurrentEnergy;
        }

        private void Update()
        {
            RefreshRestorationTimer();
        }

        private void OnDisable()
        {
            m_energyProxy.ChangedEvent -= RefreshCurrentEnergy;
        }

        private void RefreshOneEnergyRestorationSeconds()
        {
            oneEnergyRestorationSeconds.SetValueText(m_mainConfig.oneEnergyRestorationSeconds.ToString());
        }

        private void RefreshRestorationLimit()
        {
            restorationLimit.SetValueText(m_mainConfig.energyRestorationLimit.ToString());
        }

        private void RefreshRestorationTimer()
        {
            restorationTimer.SetValueText(m_energyProxy.GetRestorationTimer().ToString());
        }

        private void RefreshCurrentEnergy()
        {
            energy.SetValueText(m_energyProxy.Energy.ToString());
        }
    }
}

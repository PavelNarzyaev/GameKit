using Proxies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class EnergyPageScreen : ScreenAbstract
    {
        [SerializeField] public DebugValue energy;
        [SerializeField] public Button remove10EnergyButton;
        [SerializeField] public Button remove1EnergyButton;
        [SerializeField] public Button add1EnergyButton;
        [SerializeField] public Button add10EnergyButton;
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
            Refresh();

            m_energyProxy.ChangedEvent += Refresh;
        }

        private void OnDisable()
        {
            m_energyProxy.ChangedEvent -= Refresh;
        }

        private void Refresh()
        {
            energy.SetValueText(m_energyProxy.Energy.ToString());
        }
    }
}

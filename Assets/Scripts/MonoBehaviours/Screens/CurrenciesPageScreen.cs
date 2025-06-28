using Proxies;
using UnityEngine;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class CurrenciesPageScreen : ScreenAbstract
    {
        [SerializeField] public DebugValue coins;
        [SerializeField] public DebugValue diamonds;
        [SerializeField] public DebugValue energy;

        [Inject] private CurrenciesProxy m_currenciesProxy;

        private void Start()
        {
            coins.SetValueText(m_currenciesProxy.Soft.ToString());
            diamonds.SetValueText(m_currenciesProxy.Hard.ToString());
            energy.SetValueText(m_currenciesProxy.Energy.ToString());
        }
    }
}

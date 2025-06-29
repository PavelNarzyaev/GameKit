using Proxies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class CurrenciesPageScreen : ScreenAbstract
    {
        [SerializeField] private DebugValue coins;
        [SerializeField] private Button remove10CoinsButton;
        [SerializeField] private Button remove1CoinButton;
        [SerializeField] private Button add1CoinButton;
        [SerializeField] private Button add10CoinsButton;
        [SerializeField] private DebugValue diamonds;
        [SerializeField] private Button remove10DiamondsButton;
        [SerializeField] private Button remove1DiamondButton;
        [SerializeField] private Button add1DiamondButton;
        [SerializeField] private Button add10DiamondsButton;

        [Inject] private CurrenciesProxy m_currenciesProxy;

        private void Awake()
        {
            remove10CoinsButton.onClick.AddListener(() => m_currenciesProxy.TryToSpendSoft(10));
            remove1CoinButton.onClick.AddListener(() => m_currenciesProxy.TryToSpendSoft(1));
            add1CoinButton.onClick.AddListener(() => m_currenciesProxy.AddSoft(1));
            add10CoinsButton.onClick.AddListener(() => m_currenciesProxy.AddSoft(10));

            remove10DiamondsButton.onClick.AddListener(() => m_currenciesProxy.TryToSpendHard(10));
            remove1DiamondButton.onClick.AddListener(() => m_currenciesProxy.TryToSpendHard(1));
            add1DiamondButton.onClick.AddListener(() => m_currenciesProxy.AddHard(1));
            add10DiamondsButton.onClick.AddListener(() => m_currenciesProxy.AddHard(10));
        }

        private void OnEnable()
        {
            RefreshSoft();
            RefreshHard();

            m_currenciesProxy.SoftChangedEvent += RefreshSoft;
            m_currenciesProxy.HardChangedEvent += RefreshHard;
        }

        private void OnDisable()
        {
            m_currenciesProxy.SoftChangedEvent -= RefreshSoft;
            m_currenciesProxy.HardChangedEvent -= RefreshHard;
        }

        private void RefreshSoft()
        {
            coins.SetValueText(m_currenciesProxy.Soft.ToString());
        }

        private void RefreshHard()
        {
            diamonds.SetValueText(m_currenciesProxy.Hard.ToString());
        }
    }
}

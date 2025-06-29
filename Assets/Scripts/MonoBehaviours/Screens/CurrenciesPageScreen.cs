using Proxies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class CurrenciesPageScreen : ScreenAbstract
    {
        [SerializeField] public DebugValue coins;
        [SerializeField] public Button remove10CoinsButton;
        [SerializeField] public Button remove1CoinButton;
        [SerializeField] public Button add1CoinButton;
        [SerializeField] public Button add10CoinsButton;
        [SerializeField] public DebugValue diamonds;
        [SerializeField] public Button remove10DiamondsButton;
        [SerializeField] public Button remove1DiamondButton;
        [SerializeField] public Button add1DiamondButton;
        [SerializeField] public Button add10DiamondsButton;

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

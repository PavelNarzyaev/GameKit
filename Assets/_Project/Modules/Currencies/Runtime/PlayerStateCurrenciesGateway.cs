using System;
using GameKit.Currencies.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Currencies
{
    [UsedImplicitly]
    public class PlayerStateCurrenciesGateway
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        public event Action Changed;

        [Inject]
        private void Inject()
        {
            m_playerStateProvider.RefreshedFromJson += HandlePlayerStateRefreshedFromJson;
        }

        public int Get(CurrencyType type)
        {
            return type switch
            {
                CurrencyType.Soft => Currencies.SoftCurrency,
                CurrencyType.Hard => Currencies.HardCurrency,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Set(CurrencyType type, int value)
        {
            switch (type)
            {
                case CurrencyType.Soft:
                    Currencies.SoftCurrency = value;
                    break;
                case CurrencyType.Hard:
                    Currencies.HardCurrency = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            m_playerStateProvider.MarkAsDirty();
            Changed?.Invoke();
        }

        private PlayerCurrenciesDto Currencies => m_playerStateProvider.Data.Currencies;

        private void HandlePlayerStateRefreshedFromJson()
        {
            Changed?.Invoke();
        }
    }
}

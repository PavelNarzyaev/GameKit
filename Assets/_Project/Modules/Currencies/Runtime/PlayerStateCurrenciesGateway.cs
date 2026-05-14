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
            if (Get(type) == value)
            {
                return;
            }

            m_playerStateProvider.Edit(state =>
            {
                switch (type)
                {
                    case CurrencyType.Soft:
                        state.Currencies.SoftCurrency = value;
                        break;
                    case CurrencyType.Hard:
                        state.Currencies.HardCurrency = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            });

            Changed?.Invoke();
        }

        private PlayerCurrenciesDto Currencies => m_playerStateProvider.Data.Currencies;
    }
}

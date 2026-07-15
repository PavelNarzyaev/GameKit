using System;
using GameKit.Currencies.Contracts;
using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;
using R3;

namespace GameKit.Currencies
{
    [UsedImplicitly]
    public class PlayerStateCurrenciesGateway
    {
        private readonly IPlayerStateProvider m_playerStateProvider;

        public PlayerStateCurrenciesGateway(IPlayerStateProvider playerStateProvider)
        {
            m_playerStateProvider = playerStateProvider;
        }

        public ReadOnlyReactiveProperty<int> Get(CurrencyType type)
        {
            return type switch
            {
                CurrencyType.Soft => m_playerStateProvider.SoftCurrency,
                CurrencyType.Hard => m_playerStateProvider.HardCurrency,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Set(CurrencyType type, int value)
        {
            switch (type)
            {
                case CurrencyType.Soft:
                    m_playerStateProvider.SetSoftCurrency(value);
                    break;
                case CurrencyType.Hard:
                    m_playerStateProvider.SetHardCurrency(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}

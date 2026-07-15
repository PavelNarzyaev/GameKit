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
                CurrencyType.Soft => m_playerStateProvider.GetSoftCurrency(),
                CurrencyType.Hard => m_playerStateProvider.GetHardCurrency(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Set(CurrencyType type, int value)
        {
            if (Get(type).CurrentValue == value)
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
        }
    }
}

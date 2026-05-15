using System;
using GameKit.Currencies.Contracts;
using GameKit.Energy.Contracts;

namespace GameKit.PlayerState.Contracts
{
    [Serializable]
    public class PlayerStateDto
    {
        public string UserId { get; set; }
        public long FirstLaunchTimestamp { get; set; }
        public int LaunchesCounter { get; set; }
        public int TimeOffsetSeconds { get; set; }
        public PlayerCurrenciesDto Currencies { get; set; } = new();
        public PlayerEnergyDataDto EnergyData { get; set; } = new();
    }
}

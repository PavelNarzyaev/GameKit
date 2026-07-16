using System;
using GameKit.Currencies.Contracts;
using GameKit.Energy.Contracts;

namespace GameKit.PlayerState.Contracts
{
    [Serializable]
    public class PlayerStateDto
    {
        public PlayerStateDto(string userId = null, long firstLaunchTimestamp = 0)
        {
            UserId = userId;
            FirstLaunchTimestamp = firstLaunchTimestamp;
        }

        public string UserId { get; }
        public long FirstLaunchTimestamp { get; }
        public int LaunchesCounter { get; set; }
        public int TimeOffsetSeconds { get; set; }
        public PlayerCurrenciesDto Currencies { get; set; } = new();
        public PlayerEnergyDataDto EnergyData { get; set; } = new();
    }
}

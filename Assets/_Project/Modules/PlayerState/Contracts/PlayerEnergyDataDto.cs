using System;

namespace GameKit.PlayerState.Contracts
{
    [Serializable]
    public class PlayerEnergyDataDto
    {
        public int Energy { get; set; }
        public long NextRestoreTimestamp { get; set; }
    }
}

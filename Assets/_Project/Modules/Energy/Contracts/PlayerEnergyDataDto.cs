using System;

namespace GameKit.Energy.Contracts
{
    [Serializable]
    public class PlayerEnergyDataDto
    {
        public int Energy { get; set; }
        public long NextRestoreTimestamp { get; set; }
    }
}

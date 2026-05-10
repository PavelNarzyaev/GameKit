using System;

namespace GameKit.PlayerState
{
    [Serializable]
    public class PlayerEnergyDataDto
    {
        public int Energy { get; set; }
        public long NextRestoreTimestamp { get; set; }
    }
}

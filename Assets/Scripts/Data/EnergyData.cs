using System;

namespace Data
{
    [Serializable]
    public class EnergyData
    {
        public int energy;
        public bool isRestorationInProgress;
        public long restorationStartTimestamp;
        public int restored;
    }
}

using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public string userId;
        public long firstLaunchTimestamp;
        public int launchesCounter;
        public Currencies currencies = new();
        public EnergyData energy = new();
    }
}

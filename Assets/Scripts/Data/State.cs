using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public string userId;
        public long firstLaunchTimestamp;
        public int launchesCounter;
    }
}

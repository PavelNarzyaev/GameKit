using System;

namespace GameKit.PlayerState
{
    [Serializable]
    public class PlayerCurrenciesDto
    {
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
    }
}

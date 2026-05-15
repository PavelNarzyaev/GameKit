using System;

namespace GameKit.Currencies.Contracts
{
    [Serializable]
    public class PlayerCurrenciesDto
    {
        public int SoftCurrency { get; set; }
        public int HardCurrency { get; set; }
    }
}

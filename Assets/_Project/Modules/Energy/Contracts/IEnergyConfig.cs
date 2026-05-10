namespace GameKit.Energy.Contracts
{
    public interface IEnergyConfig
    {
        int OneEnergyRestorationSeconds { get; }
        int EnergyRestorationLimit { get; }
    }
}

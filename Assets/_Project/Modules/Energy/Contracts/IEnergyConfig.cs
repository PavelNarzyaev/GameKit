namespace GameKit.Energy
{
    public interface IEnergyConfig
    {
        int OneEnergyRestorationSeconds { get; }
        int EnergyRestorationLimit { get; }
    }
}

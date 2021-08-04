namespace AirportSimulatorLogic.Interfaces
{
    public interface IStationBuilder
    {
        IStation CreateStation(IStationConfig config);
    }
}

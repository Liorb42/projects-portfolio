namespace AirportSimulatorLogic.Interfaces
{
    public interface IControlTowerBuilder
    {
        void Reset();
        void AddLandingBuilder();
        void AddTakeOffBuilder();
        void AddFlightGenerator();
        IControlTower GetResult();

    }
}

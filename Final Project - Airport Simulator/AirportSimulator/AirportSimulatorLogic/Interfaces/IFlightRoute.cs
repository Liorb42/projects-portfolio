namespace AirportSimulatorLogic.Interfaces
{
    public interface IFlightRoute
    {
        int Id { get; set; }          
        bool IsDone { get; set; }
        void AddStation(ref IStation station);
        void SetNextForStation(int stationId, int nextStationId);
        void SetStartingPositions(params int[] stationsId);
        IStation GetCurrentStation();
        int GetCurrentStationId();
        IStation GetNextStation();
        int GetNextStationId();
        void MoveToNextStation();
        void UpdateLeaveCurrentStation();
        IFlightRoute Clone();
        void UpdateCurrentStationIsDone();
    }
}
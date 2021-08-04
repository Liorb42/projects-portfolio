using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;
using System;
using System.Threading;

namespace AirportSimulatorLogic.BL
{
    public class FlightMovement : IFlightMovement
    {
        private IStation nextStation;
        public int Id { get; set; }
        public IFlight Flight { get; set; }
        public IFlightRoute Route { get; set; }
        public int CurrentStationId { get; set; }

        public event Action ConfirmMoveIsDoneEvent;
        public event Action<IFlightMovement> InformReadyForNextEvent;
        public event Action<IFlightMovement> InformFlightIsDoneEvent;

        public FlightMovement(IFlight flight)
        {
            this.Flight = flight;
            this.Id = flight.Number;
        }
        public void EnableMoveSequence()
        {          
            //check if route is done
            if (Route?.IsDone ?? false)
            {
                Route?.UpdateLeaveCurrentStation();
                InformFlightIsDoneEvent?.Invoke(this);
                return;
            }

            //get the next station
            nextStation = Route?.GetNextStation();
           
            if (nextStation != null) TryEnterStation();
        }       
        private void TryEnterStation()
        {
            bool isWaiting = true;

            while(isWaiting)
            {
                if (nextStation.GetPermissionToEnter(Flight))
                {
                    isWaiting = false;

                    //leave current station
                    Route?.UpdateLeaveCurrentStation();
                    Route?.MoveToNextStation();
                    Flight.CurrentStationId = nextStation.Id;

                    //enter next station
                    CurrentStationId = nextStation.Id;
                    nextStation.StayDurationOverEvent += InformReadyForNext;
                    nextStation.StartStayTimer();

                    //confirm to tower only after all changes are done
                    ConfirmMoveIsDoneEvent?.Invoke();
                }
                else
                {
                    //wait and try again
                    Thread.Sleep(5000);
                }
            }           
        }    
        private void InformReadyForNext()
        {
            nextStation.StayDurationOverEvent -= InformReadyForNext;
            Route.UpdateCurrentStationIsDone();
            InformReadyForNextEvent?.Invoke(this);                 
        }
    }
}

using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;
using System;
using System.Timers;

namespace AirportSimulatorLogic.BL
{
    public class Station : IStation, IDisposable
    {
        private readonly Timer timer;
        private IFlightMovement HoldingFlightMovement;
        public event Action StationOpenEvent;
        public event Action StayDurationOverEvent;


        public int Id { get; set; }
        public IFlight Flight { get; set; }
        public TimeSpan TotalDurationOfStay { get; set; }
        public DateTime StayStartTime { get; set; }
        public bool IsOpen { get; set; }
        public bool IsHoldForLandings { get; }
        public bool IsHoldForTakeOffs { get; }
        public bool IsLandingHoldNeeded { get; }
        public bool IsTakeOffHoldNeeded { get; }


        public Station(IStationConfig config)
        {
            this.Id = config.StationId;
            this.Flight = null;
            this.TotalDurationOfStay = config.TotalDurationOfStay;
            this.IsOpen = true;
            timer = new Timer(TotalDurationOfStay.TotalMilliseconds);
            timer.Elapsed += (s,e)=> OnStayDurationOver();
            timer.AutoReset = false;
        }
        public bool GetPermissionToEnter(IFlight flight)
        {
            //if there is a hold and the flight is in the oposite direction to the hold, refuse entrence
            if (HoldingFlightMovement != null
            && flight.IsLanding != HoldingFlightMovement.Flight.IsLanding)
                return false;
          
            //if the station has no flight allow to proceed
            if (Flight == null)
            {
                //if this is the flight that is holding the station - release hold.
                if (HoldingFlightMovement != null
               && flight.Number == HoldingFlightMovement.Flight.Number)
                    HoldingFlightMovement = null;

                IsOpen = false;
                Flight = flight;
              
                return true;
            }
            return false;
        }
        private void OnStayDurationOver()
        {
            timer.Stop();
            StayDurationOverEvent?.Invoke();
        }       
        public void StartStayTimer()
        {
            StayStartTime = DateTime.Now;
            timer.Start();
        }
        public void FlightLeftStation()
        {
            Flight = null;
            IsOpen = true;
            StayStartTime = default;
        }
        public bool HoldStation(IFlightMovement flightMovement)
        {
            if(HoldingFlightMovement == null)
            {
                HoldingFlightMovement = flightMovement;
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
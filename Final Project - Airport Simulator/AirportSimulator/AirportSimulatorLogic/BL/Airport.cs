using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;
using AirportSimulatorShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportSimulatorLogic.BL
{
    public class Airport : IAirport
    {
        private readonly IControlTower tower;
        public event Action<IAirportState> UpdateStateEvent;
        private readonly object lockObjState = new object();

        public Airport(IControlTower tower)
        {
            this.tower = tower;
            this.tower.UpdateStateEvent += UpdateState;           
        }        
        public IFlightRoute GetLandingRoute()
        {
            return tower.LandingRoute;
        }
        public void SetLandingRoute(IFlightRoute newRoute)
        {
            tower.LandingRoute = newRoute;
        }
        public IFlightRoute GetTakeOffRoute()
        {
            return tower.TakeOffRoute;
        }
        public void SetTakeOffRoute(IFlightRoute newRoute)
        {
            tower.TakeOffRoute = newRoute;
        }
        public IEnumerable<IFlight> GetLandings()
        {
            foreach (var landing in tower.Landings)
            {
                yield return landing.Flight;
            }
        }
        public void SetState(IAirportState state)
        {
            if(state.Landings != null)
                tower.SetLandingsState(state.Landings);
            if(state.TakeOffs != null)
                tower.SetTakeOffsState(state.TakeOffs);
        }
        public IEnumerable<IFlight> GetTakeOffs()
        {
            foreach (var takeOff in tower.TakeOffs)
            {
                yield return takeOff.Flight;
            }
        }
        public IEnumerable<IStationState> GetStations()
        {
            foreach (var station in tower.Stations)
            {
                IStationState stationState = new StationState
                {
                    Number = station.Id,
                    CurrentFlightId = station.Flight?.Number ?? 0,
                    IsFlightLanding = station.Flight?.IsLanding ?? false,
                    IsFlightTakingOff = station.Flight?.IsTakingOff ?? false,
                    StayStartTime = station.StayStartTime,
                    TotalDurationOfStay = station.TotalDurationOfStay                 
                };
                yield return stationState;
            }
        }             
        private void UpdateState()
        {
            lock(lockObjState)
            {
                UpdateStateEvent?.Invoke(new AirportState
                {
                    Landings = GetLandings().ToList(),
                    TakeOffs = GetTakeOffs().ToList(),
                    Stations = GetStations().ToList()
                });
            }            
        }
        public IAirportState GetState()
        {
            lock (lockObjState)
            {
                return new AirportState
                {
                    Landings = GetLandings().ToList(),
                    TakeOffs = GetTakeOffs().ToList(),
                    Stations = GetStations().ToList()
                };
            }
        }
    }
}

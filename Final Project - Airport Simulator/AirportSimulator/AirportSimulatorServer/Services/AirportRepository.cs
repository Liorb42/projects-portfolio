using AirportSimulatorServer.Data;
using AirportSimulatorShared.Interfaces;
using AirportSimulatorShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportSimulatorServer.Services
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportStateContext context;

        public AirportRepository(AirportStateContext context)
        {
            this.context = context;
        }
        public void UpdateState(IAirportState state)
        {
            foreach (var landing in state.Landings)
            {
                UpdateFlight(landing);
            }
            foreach (var takeOff in state.TakeOffs)
            {
                UpdateFlight(takeOff);
            }
            foreach (var station in state.Stations)
            {
                UpdateStation(station);
            }
        }
        public void UpdateFlight(IFlight flight)
        {
            IFlight flightInDb = context.Flights.SingleOrDefault(f => f.Number == flight.Number);
            if(flightInDb == null)
            {
                context.Flights.Add((Flight)flight);
            }
            else
            {
                flightInDb.CurrentStationId = flight.CurrentStationId;
            }
            context.SaveChanges();
        }     
        public void UpdateStation(IStationState station)
        {
            IStationState stationInDb = context.Stations.SingleOrDefault(s => s.Number == station.Number);
            if (stationInDb == null)
            {
                context.Stations.Add((StationState)station);                
            }
            else
            {
                stationInDb.CurrentFlightId = station.CurrentFlightId;
                stationInDb.StayStartTime = station.StayStartTime;
                stationInDb.IsFlightLanding = station.IsFlightLanding;
                stationInDb.IsFlightTakingOff = station.IsFlightTakingOff;
            }
            context.SaveChanges();
        }
        public IEnumerable<IFlight> GetAllFututreLandings()
        {
            return context.Flights.Where(flight => flight.IsLanding && flight.LandingTime > DateTime.Now);
        }
        public IEnumerable<IFlight> GetAllFututreTakeOffs()
        {
            return context.Flights.Where(flight => flight.IsTakingOff && flight.TakeOffTime > DateTime.Now);
        }
        public IEnumerable<IStationState> GetAllStations()
        {
            return context.Stations.ToList();
        }      
    }
}

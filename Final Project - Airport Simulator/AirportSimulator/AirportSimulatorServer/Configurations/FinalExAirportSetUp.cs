using AirportSimulatorLogic.BL;
using AirportSimulatorLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace AirportSimulatorServer.Configurations
{
    public class FinalExAirportSetUp
    {
        private readonly IAirportConfig config;       
        public List<IStation> Stations { get; set; }

        public FinalExAirportSetUp()
        {
            this.config = new AirportConfig(8);
            Stations = new List<IStation>();           
        }
        public IStationConfig[] GetStationsConfig()
        {
            IStationConfig s1Config = new StationConfig { StationId = 1, TotalDurationOfStay = new TimeSpan(0, 0, 15) };
            IStationConfig s2Config = new StationConfig { StationId = 2, TotalDurationOfStay = new TimeSpan(0, 0, 15) };
            IStationConfig s3Config = new StationConfig { StationId = 3, TotalDurationOfStay = new TimeSpan(0, 0, 15) };
            IStationConfig s4Config = new StationConfig { StationId = 4, TotalDurationOfStay = new TimeSpan(0, 0, 15), IsLandingHoldNeeded = true };
            IStationConfig s5Config = new StationConfig { StationId = 5, TotalDurationOfStay = new TimeSpan(0, 0, 15) };
            IStationConfig s6Config = new StationConfig { StationId = 6, TotalDurationOfStay = new TimeSpan(0, 0, 15), IsHoldForLandings = true};
            IStationConfig s7Config = new StationConfig { StationId = 7, TotalDurationOfStay = new TimeSpan(0, 0, 15), IsHoldForLandings = true };
            IStationConfig s8Config = new StationConfig { StationId = 8, TotalDurationOfStay = new TimeSpan(0, 0, 15) };

            config.StationsConfig[0] = s1Config;
            config.StationsConfig[1] = s2Config;
            config.StationsConfig[2] = s3Config;
            config.StationsConfig[3] = s4Config;
            config.StationsConfig[4] = s5Config;
            config.StationsConfig[5] = s6Config;
            config.StationsConfig[6] = s7Config;
            config.StationsConfig[7] = s8Config;

            return config.StationsConfig;
        }
        public IFlightRoute GetTakeOffRoute()
        {
            return config.TakeOffRoute;
        }
        public IFlightRoute GetLandingRoute()
        {
            return config.LandingRoute;
        }
        public void GenerateRoutes()
        {
            CreateLandingRoute();
            CreateTakeOffRoute();
        }
        private void CreateLandingRoute()
        {
            IFlightRoute landingRoute = new FlightRoute
            {
                Id = 1
            };

            IStation s1 = Stations.Find(s => s.Id == 1);
            IStation s2 = Stations.Find(s => s.Id == 2);
            IStation s3 = Stations.Find(s => s.Id == 3);
            IStation s4 = Stations.Find(s => s.Id == 4);
            IStation s5 = Stations.Find(s => s.Id == 5);
            IStation s6 = Stations.Find(s => s.Id == 6);
            IStation s7 = Stations.Find(s => s.Id == 7);

            landingRoute.AddStation(ref s1);
            landingRoute.AddStation(ref s2);
            landingRoute.AddStation(ref s3);
            landingRoute.AddStation(ref s4);
            landingRoute.AddStation(ref s5);
            landingRoute.AddStation(ref s6);
            landingRoute.AddStation(ref s7);

            landingRoute.SetNextForStation(1, 2);
            landingRoute.SetNextForStation(2, 3);
            landingRoute.SetNextForStation(3, 4);
            landingRoute.SetNextForStation(4, 5);
            landingRoute.SetNextForStation(5, 6);
            landingRoute.SetNextForStation(5, 7);

            landingRoute.SetStartingPositions(1);
            this.config.LandingRoute = landingRoute;
        }
        private void CreateTakeOffRoute()
        {
            IFlightRoute takeOffRoute = new FlightRoute
            {
                Id = 2
            };

            IStation s4 = Stations.Find(s => s.Id == 4);
            IStation s6 = Stations.Find(s => s.Id == 6);
            IStation s7 = Stations.Find(s => s.Id == 7);
            IStation s8 = Stations.Find(s => s.Id == 8);

            takeOffRoute.AddStation(ref s4);
            takeOffRoute.AddStation(ref s6);
            takeOffRoute.AddStation(ref s7);
            takeOffRoute.AddStation(ref s8);

            takeOffRoute.SetNextForStation(7, 8);
            takeOffRoute.SetNextForStation(6, 8);
            takeOffRoute.SetNextForStation(8, 4);

            takeOffRoute.SetStartingPositions(6, 7);

            config.TakeOffRoute = takeOffRoute;
        }


    }

    
}

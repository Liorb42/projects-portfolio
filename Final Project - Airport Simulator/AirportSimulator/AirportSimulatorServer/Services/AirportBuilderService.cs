using AirportSimulatorLogic.BL;
using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;
using AirportSimulatorServer.Configurations;
using System.Linq;

namespace AirportSimulatorServer.Services
{
    public class AirportBuilderService : IAirportBuilderService
    {
        private IAirport currentAirport;

        private readonly IAirportConfig config;
        private IControlTowerBuilder towerBuilder;
        private IControlTower tower;
        private IStationBuilder stationBuilder;       
        private readonly IFlightGenerator flightGenerator;
        private readonly FinalExAirportSetUp setUp;

        public AirportBuilderService(IFlightGenerator flightGenerator)
        {            
            this.flightGenerator = flightGenerator;
            this.setUp = new FinalExAirportSetUp();
            this.config = new AirportConfig(8)
            {
                StationsConfig = this.setUp.GetStationsConfig()
            };
            AddControlTower();
            AddStations();
            this.setUp.Stations = this.tower.Stations.ToList();
            this.setUp.GenerateRoutes();
            this.config.LandingRoute = this.setUp.GetLandingRoute();
            SetLandingRoute();
            this.config.TakeOffRoute = this.setUp.GetTakeOffRoute();
            SetTakeOffRoute();
            CreateAirport();
        }      
        private void AddControlTower()
        {
            towerBuilder = new ControlTowerBuilder(flightGenerator);
            towerBuilder.AddLandingBuilder();
            towerBuilder.AddTakeOffBuilder();
            tower = towerBuilder.GetResult();
        }
        private void AddStations()
        {
            stationBuilder = new StationBuilder();
            foreach (var config in config.StationsConfig)
            {
                IStation station = stationBuilder.CreateStation(config);
                tower.AddStation(ref station);
            }    
        }
        private void CreateAirport()
        {
            if (config != null && stationBuilder != null && tower != null)
            {
                currentAirport = new Airport(tower);                          
            }
        }
        public IAirport GetAirport()
        {
            return currentAirport;
        }     
        private void SetLandingRoute()
        {
            tower.LandingRoute = config.LandingRoute;   
        }
        private void SetTakeOffRoute()
        {
            tower.TakeOffRoute = config.TakeOffRoute;
        }        
    }
}

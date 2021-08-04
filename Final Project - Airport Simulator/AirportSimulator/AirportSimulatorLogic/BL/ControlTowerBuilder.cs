using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;

namespace AirportSimulatorLogic.BL
{
    public class ControlTowerBuilder : IControlTowerBuilder
    {
        private readonly IFlightGenerator flightGenerator;
        private ILandingBuilder landingBuilder;
        private ITakeOffBuilder takeOffBuilder;

        public ControlTowerBuilder(IFlightGenerator flightGenerator)
        {            
            this.flightGenerator = flightGenerator;
            Reset();
        }
        public void AddFlightGenerator()
        {
            //do nothing as in this implentation the flight generator will be provided by DI
        }       
        public void AddLandingBuilder()
        {
            landingBuilder = new LandingBuilder();
        }
        public void AddTakeOffBuilder()
        {
            takeOffBuilder = new TakeOffBuilder();
        }
        public IControlTower GetResult()
        {
            if (landingBuilder != null && takeOffBuilder != null && flightGenerator != null)
            {
                IControlTower tower = new Tower(flightGenerator,landingBuilder,takeOffBuilder);
                Reset();
                return tower;
            }

            else return null;
        }
        public void Reset()
        {
            landingBuilder = null;
            takeOffBuilder = null;
        }
    }
}

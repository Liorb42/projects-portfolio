using AirportSimulatorShared.Interfaces;
using System.Collections.Generic;

namespace AirportSimulatorShared.Models
{

    public class AirportState : IAirportState
    {
        public AirportState()
        {
            Landings = new List<IFlight>();
            TakeOffs = new List<IFlight>();
            Stations = new List<IStationState>();          
        }        
  
        public int Id { get; set; }       
        public IEnumerable<IFlight> Landings { get;set; }
        public IEnumerable<IFlight> TakeOffs { get; set; }
        public IEnumerable<IStationState> Stations { get; set; }
    }
}

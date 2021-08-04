using AirportSimulatorLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AirportSimulatorLogic.BL
{
    public class FlightRouteNode : IFightRouteNode
    {
        private List<IFightRouteNode> nextStationNodes;

        public IStation CurrentStation { get; set; }
        public IEnumerable<IFightRouteNode> NextStationNodes
        {
            get { return nextStationNodes; }
            set { nextStationNodes = value.ToList(); }
        }
        public bool IsDone { get; set; }

        public FlightRouteNode(IStation station)
        {
            CurrentStation = station;
            nextStationNodes = new List<IFightRouteNode>();
        }       
    }
}
using System.Collections.Generic;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IFightRouteNode
    {
        IStation CurrentStation { get; }
        IEnumerable<IFightRouteNode> NextStationNodes { get; set; }
        bool IsDone { get; set; }

    }
}

using AirportSimulatorShared.Interfaces;
using System.Threading.Tasks;

namespace AirportSimulatorServer.Api.Clients
{
    public interface IAirportClient
    {
        Task ReceiveAirportState(IAirportState state);
    }
}

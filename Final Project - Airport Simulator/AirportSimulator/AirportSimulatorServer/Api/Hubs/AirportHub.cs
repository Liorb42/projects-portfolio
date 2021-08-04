using AirportSimulatorServer.Api.Clients;
using AirportSimulatorShared.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AirportSimulatorServer.Api.Hubs
{
    public class AirportHub : Hub<IAirportClient>
    {
        public async Task SendAirportState(IAirportState state)
        {
            await Clients.All.ReceiveAirportState(state);
        }

    }
}

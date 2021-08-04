using AirportSimulatorLogic.Interfaces;
using AirportSimulatorServer.Api.Clients;
using AirportSimulatorServer.Api.Hubs;
using AirportSimulatorShared.Interfaces;
using AirportSimulatorShared.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimulatorServer.Services
{
    public class AirportService : IAirportService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private IAirportRepository airportRepository;
        private readonly IAirportBuilderService airportBuilderService;
        private readonly IHubContext<AirportHub, IAirportClient> airportHubContext;
        private IAirport currentAirport;
        private readonly object lockObjRepository = new object();
        private readonly object lockObjUi = new object();

        public AirportService(
            IServiceScopeFactory scopeFactory, 
            IAirportBuilderService airportBuilderService, 
            IHubContext<AirportHub, IAirportClient> airportHubContext)
        {
            this.scopeFactory = scopeFactory;
            this.airportBuilderService = airportBuilderService;
            this.airportHubContext = airportHubContext;
            Task.Run (()=> Start());
        }
        public async Task Start()
        {
            if (currentAirport == null)
            {
                await Task.Run(()=>CreateAirport());
                RegisterToStateChangeEvent();
            }
        }
        private void CreateAirport()
        {
            currentAirport = airportBuilderService.GetAirport();
            using(var scope = scopeFactory.CreateScope())
            {
                IAirportState state = new AirportState();
                lock (lockObjRepository)
                {
                    airportRepository = scope.ServiceProvider.GetRequiredService<IAirportRepository>();
                    
                    state.Landings = airportRepository.GetAllFututreLandings().ToList();
                    state.TakeOffs = airportRepository.GetAllFututreTakeOffs().ToList();     
                }
                currentAirport.SetState(state);
            }   
            UpdateRepository(currentAirport.GetState());
        }
        private void RegisterToStateChangeEvent()
        {
            currentAirport.UpdateStateEvent += UpdateRepository;
            currentAirport.UpdateStateEvent += UpdateUI;
        }
        private void UpdateRepository(IAirportState state)
        {
            lock (lockObjRepository)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    airportRepository = scope.ServiceProvider.GetRequiredService<IAirportRepository>();

                    airportRepository.UpdateState(state);
                } 
            }
        }
        private void UpdateUI(IAirportState state)
        {
            lock (lockObjUi)
            {
                this.airportHubContext.Clients.All.ReceiveAirportState(state); 
            }
        }

    }
}

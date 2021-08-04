using AirportSimulatorServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AirportSimulatorServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportSimulatorController : ControllerBase
    {
        private readonly IAirportService airportService;        

        public AirportSimulatorController(IAirportService airportService)
        {
            this.airportService = airportService;           
        }
       
        [HttpGet]
        public async Task<string> Start()
        {
            await airportService.Start();
            return "Airport Simulator Server initialized";
        }          
    }
}

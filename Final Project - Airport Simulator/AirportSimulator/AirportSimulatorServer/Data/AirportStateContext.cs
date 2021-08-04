using AirportSimulatorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportSimulatorServer.Data
{
    public class AirportStateContext : DbContext
    {
        public AirportStateContext(DbContextOptions<AirportStateContext> options) : base(options)
        {
        }
        public DbSet<Flight>Flights { get; set; }
        public DbSet<StationState>Stations { get; set; }
    }
}

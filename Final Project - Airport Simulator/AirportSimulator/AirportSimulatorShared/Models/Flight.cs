using AirportSimulatorShared.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSimulatorShared.Models
{
    public class Flight : IFlight
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime TakeOffTime { get; set; }
        public DateTime LandingTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsLanding { get; set; }
        public bool IsTakingOff { get; set; }

        [ForeignKey("Stations")]
        public int CurrentStationId { get; set; }
    }
}

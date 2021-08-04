using AirportSimulatorShared.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSimulatorShared.Models
{
    [Table("Stations")]
    public class StationState : IStationState
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }

        [ForeignKey ("Flights")]
        public int CurrentFlightId { get; set; }
        public TimeSpan TotalDurationOfStay { get; set; }
        public DateTime StayStartTime { get; set; }
        public bool IsFlightLanding { get ; set; }
        public bool IsFlightTakingOff { get ; set; }
    }
}

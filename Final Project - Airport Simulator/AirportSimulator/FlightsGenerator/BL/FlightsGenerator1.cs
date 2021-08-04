using AirportSimulatorShared.Interfaces;
using AirportSimulatorShared.Models;
using System;
using System.Timers;

namespace FlightsGenerator.BL
{
    public class FlightsGenerator1 : IFlightGenerator, IDisposable
    {
        private TimeSpan landingsInterval;
        private TimeSpan takeOffsInterval;
        private readonly Timer landingTimer;
        private readonly Timer takeOffTimer;
        private readonly Random rnd;
        private const int randomHourLowRange = 2;
        private const int randomHourHighRange = 12;
        private int counter = 100000;
        private readonly string[] airports = { "JFK", "LAX", "SFO", "AMS", "BCN", "BRU", "CDG", "CIA" };
        private readonly object obj = new object();
        public TimeSpan LandingsInterval
        {
            get
            {
                return landingsInterval;
            }
            set
            {
                landingsInterval = value;
                landingTimer.Stop();
                landingTimer.Interval = landingsInterval.TotalMilliseconds;
            }
        }
        public TimeSpan TakeOffsInterval
        {
            get
            {
                return takeOffsInterval;
            }
            set
            {
                takeOffsInterval = value;
                takeOffTimer.Stop();
                takeOffTimer.Interval = takeOffsInterval.TotalMilliseconds;
            }
        }

        public event Action<IFlight> NewLandingFlightEvent;
        public event Action<IFlight> NewTakeOffFlightEvent;

        public FlightsGenerator1(TimeSpan? landingsInterval = null, TimeSpan? takeOffsInterval = null)
        {
            var realLandingsInterval = landingsInterval ?? new TimeSpan(0, 0, 30);
            var realTakeOffInterval = takeOffsInterval ?? new TimeSpan(0, 0, 33);
           
            this.landingTimer = new Timer(realLandingsInterval.TotalMilliseconds);
            this.takeOffTimer = new Timer(realTakeOffInterval.TotalMilliseconds);
            LandingsInterval = realLandingsInterval;
            TakeOffsInterval = realTakeOffInterval;
            rnd = new Random();           
            landingTimer.Elapsed += ((s, e) => GenerateLanding());
            takeOffTimer.Elapsed += ((s, e) => GenerateTakeOff());
            landingTimer.AutoReset = true;
            takeOffTimer.AutoReset = true;
            landingTimer.Start();
            takeOffTimer.Start();
        }
        private void GenerateLanding()
        {
            IFlight flight = GenerateFlight();            
            flight.IsLanding = true;
            flight.Destination = "TLV";
            flight.TakeOffTime = DateTime.Now - (new TimeSpan(rnd.Next(randomHourLowRange, randomHourHighRange), 0, 0));
            flight.LandingTime = DateTime.Now;
            NewLandingFlightEvent?.Invoke(flight);   
        }
        private void GenerateTakeOff()
        {
            IFlight flight = GenerateFlight();            
            flight.IsTakingOff = true;
            flight.Origin = "TLV";
            flight.TakeOffTime = DateTime.Now;
            flight.LandingTime = DateTime.Now + (new TimeSpan(rnd.Next(randomHourLowRange, randomHourHighRange), 0, 0));
            NewTakeOffFlightEvent?.Invoke(flight);
        }
        private IFlight GenerateFlight()
        {
            IFlight flight = new Flight();
            lock (obj)
            {
                flight.Number = ++counter;
            }
            flight.Origin = airports[rnd.Next(0, airports.Length)];
            flight.Destination = airports[rnd.Next(0, airports.Length)];
            return flight;
        }    
        public void Dispose()
        {
            this.landingTimer.Dispose();
            this.takeOffTimer.Dispose();
        }
    }
}

using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSimulatorLogic.BL
{
    public class Tower : IControlTower
    {
        private readonly IFlightGenerator flightGenerator;
        private readonly ILandingBuilder landingBuilder;
        private readonly ITakeOffBuilder takeOffBuilder;
        private readonly List<IFlightMovement> landings;
        private readonly List<IFlightMovement> takeOffs;
        private readonly List<IStation> stations;
        private readonly object lockObjLandings = new object();
        private readonly object lockObjTakeOffs = new object();

        public IEnumerable<IFlightMovement> Landings
        {
            get
            {
                lock (lockObjLandings)
                { return landings; }
            }
        }
        public IEnumerable<IFlightMovement> TakeOffs
        {
            get
            {
                lock (lockObjTakeOffs)
                { return takeOffs; }
            }
        }
        public IEnumerable<IStation> Stations { get => stations; }
        public IFlightRoute LandingRoute { get; set; } 
        public IFlightRoute TakeOffRoute { get; set; } 

        public event Action UpdateStateEvent;

        public Tower(
            IFlightGenerator flightGenerator, 
            ILandingBuilder landingBuilder, 
            ITakeOffBuilder takeOffBuilder
            )
        {
            this.flightGenerator = flightGenerator;
            this.landingBuilder = landingBuilder;
            this.takeOffBuilder = takeOffBuilder;
            this.landings = new List<IFlightMovement>();
            this.takeOffs = new List<IFlightMovement>();
            this.stations = new List<IStation>();
            this.flightGenerator.NewLandingFlightEvent += RecieveNewLandingFlight;
            this.flightGenerator.NewTakeOffFlightEvent += RecieveNewTakeOffFlight;
        }

        private void RecieveNewLandingFlight(IFlight flight)
        {
            IFlightMovement landing = CreateFlightMovments(flight);
            landing.Route = LandingRoute.Clone();
            landings.Add(landing);
            Task.Run(() => StartLanding(landing));
        }      
        private void RecieveNewTakeOffFlight(IFlight flight)
        {
            IFlightMovement takeOff = CreateFlightMovments(flight);
            takeOff.Route = TakeOffRoute.Clone();
            takeOffs.Add(takeOff);
            Task.Run(() => StartTakeOff(takeOff));            
        }
        private IFlightMovement CreateFlightMovments(IFlight flight)
        {
            IFlightMovement fm = null;
            if (flight.IsLanding)
                fm = landingBuilder.CreateLanding(flight);
            if (flight.IsTakingOff)
                fm = takeOffBuilder.CreateTakeOff(flight);
            fm.ConfirmMoveIsDoneEvent += UpdateState;
            fm.InformReadyForNextEvent += MoveToNextStation;
            fm.InformFlightIsDoneEvent += EliminateFlight;
            return fm;
        }
        public void AddStation(ref IStation station)
        {
            stations.Add(station);
        }
        public void RemoveStation(IStation station)
        {
            stations.Remove(stations.Find(s => s.Id == station.Id));

        }
        private void StartLanding(IFlightMovement landing)
        {
            MoveToNextStation(landing);
        }
        private void StartTakeOff(IFlightMovement takeOff)
        {
            MoveToNextStation(takeOff);
        }
        private void MoveToNextStation(IFlightMovement flightMovement)
        {
            CheckIfHoldIsNeeded(flightMovement);
            flightMovement.EnableMoveSequence();            
        }
        private void CheckIfHoldIsNeeded(IFlightMovement flightMovement)
        {
            var nextStation = flightMovement.Route.GetNextStation();
            //check if the a hold needs to be set before the flight enters this station
            if (nextStation != null)
            {
                bool isWaiting = true;

                while(isWaiting)
                {
                    if(!nextStation.IsLandingHoldNeeded || !nextStation.IsTakeOffHoldNeeded) isWaiting = false;
                    //for landings
                    if (flightMovement.Flight.IsLanding && nextStation.IsLandingHoldNeeded)
                    {
                        //find the first station to hold and activate hold method
                        if (stations.Find(s => s.IsHoldForLandings).HoldStation(flightMovement)) isWaiting = false;
                    }
                    //for takeOff
                    if (flightMovement.Flight.IsTakingOff && nextStation.IsTakeOffHoldNeeded)
                    {
                        if(stations.Find(s => s.IsHoldForTakeOffs).HoldStation(flightMovement)) isWaiting = false;
                    }
                }                
            }         
        }
        private void UpdateState()
        { 
          UpdateStateEvent?.Invoke();
        }       
        private void EliminateFlight(IFlightMovement flightMovement)
        {
            if (flightMovement.Flight.IsLanding) RemoveLandingFromList(flightMovement);
            else RemoveTakeOffFromList(flightMovement);
            UpdateState();
        }
        private void RemoveLandingFromList(IFlightMovement flightMovement)
        {
            lock (lockObjLandings)
            {
                int flightInx = landings.FindIndex(l => l.Id == flightMovement.Id);
                if (flightInx != -1) landings.RemoveAt(flightInx); 
            }
        }
        private void RemoveTakeOffFromList(IFlightMovement flightMovement)
        {
            lock (lockObjTakeOffs)
            {
                int flightInx = takeOffs.FindIndex(t => t.Id == flightMovement.Id);
                if (flightInx != -1) takeOffs.RemoveAt(flightInx); 
            }
        }
        public void SetLandingsState(IEnumerable<IFlight> landings)
        {
            foreach (var flight in landings)
            {
                IFlightMovement landing = CreateFlightMovments(flight);
                landing.Route.SetStartingPositions(flight.CurrentStationId);
                this.landings.Add(landing);
                Task.Run(() => StartLanding(landing));
            }
        }
        public void SetTakeOffsState(IEnumerable<IFlight> takeOffs)
        {
            foreach (var flight in takeOffs)
            {
                IFlightMovement takeOff = CreateFlightMovments(flight);
                takeOff.Route.SetStartingPositions(flight.CurrentStationId);
                this.takeOffs.Add(takeOff);
                Task.Run(() => StartLanding(takeOff));
            }
        }
       
    }
}
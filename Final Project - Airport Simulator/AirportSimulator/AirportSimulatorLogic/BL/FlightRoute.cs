using AirportSimulatorLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AirportSimulatorLogic.BL
{
    public class FlightRoute : IFlightRoute
    {
        private bool isRouteStarted;
        private readonly List<IFightRouteNode> stationNodes;
        private readonly List<IFightRouteNode> startPositionNodes;
        private IFightRouteNode currentStationNode;
        private IFightRouteNode nextStationNode;

        public int Id { get; set; }
        public bool IsDone { get; set; }
        public List<IFightRouteNode> StationNodes { get => stationNodes; }
        public IFightRouteNode CurrentStationNode { get => currentStationNode; }
        public IFightRouteNode NextStationNode { get => nextStationNode; }

        public FlightRoute()
        {
            this.stationNodes = new List<IFightRouteNode>();
            this.startPositionNodes = new List<IFightRouteNode>();
            this.isRouteStarted = false;
            this.currentStationNode = null;
            this.nextStationNode = null;
            this.IsDone = false;
        }
        public void AddStation(ref IStation station)
        {
            stationNodes.Add(new FlightRouteNode(station));
        }
        public void RemoveStation(IStation station)
        {
            stationNodes.Remove(stationNodes.Find(sn => sn.CurrentStation.Id == station.Id));
        }
        public void SetNextForStation(int stationId, int nextStationId)
        {
            IFightRouteNode nextNode = stationNodes.Find(s => s.CurrentStation.Id == nextStationId);
            List<IFightRouteNode> nextList = new List<IFightRouteNode>
            {
                nextNode
            };
            stationNodes.Find(s => s.CurrentStation.Id == stationId).NextStationNodes = nextList;
        }
        public void SetStartingPositions(params int[] stationsId)
        {
            foreach (var id in stationsId)
            {
                IFightRouteNode startPosition = stationNodes.Find(n => n.CurrentStation.Id == id);
                startPositionNodes.Add(startPosition);
            }
        }
        private void SetStartNode()
        {
            isRouteStarted = true;
            while (nextStationNode == null)
            {
                if (startPositionNodes != null)
                {
                    foreach (var node in startPositionNodes)
                    {                        
                        if (node.CurrentStation.IsOpen)
                        {
                            nextStationNode = node;
                            break;
                        }                        
                    }
                }
            }            
        }
        public IStation GetCurrentStation()
        {
            if (isRouteStarted)
                return currentStationNode?.CurrentStation;
            return null;
        }
        public int GetCurrentStationId()
        {
            return GetCurrentStation().Id;
        }
        public IStation GetNextStation()
        {
            if (!isRouteStarted)
            {
                SetStartNode();
                return nextStationNode.CurrentStation;
            }
            if (IsDone)
            {
                return null;
            }
            if(NextStationNode == null)
            {
                List<IFightRouteNode> nextStationNodes = currentStationNode?.NextStationNodes?.ToList();

                if (nextStationNodes == null) return null;
               
                else
                {
                    bool isWaiting = true;

                    while (isWaiting)
                    {
                        foreach (var node in nextStationNodes)
                        {
                            if (node.CurrentStation.IsOpen)
                            {
                                isWaiting = false;
                                nextStationNode = node;
                                return nextStationNode.CurrentStation;
                            }
                        }
                    }                    
                }
            }           
            return NextStationNode.CurrentStation;
        }
        public int GetNextStationId()
        {
            IStation next = GetNextStation();
            return next?.Id ?? 0;
        }
        private IFightRouteNode GetNextStationNode()
        {
            GetNextStation();
            return nextStationNode;
        }
        public void MoveToNextStation()
        {
            //make sure nextNode is not null
            if (nextStationNode == null) GetNextStationNode();
            //set the next to be the current
            currentStationNode = nextStationNode;
            nextStationNode = null;
        }
        public void UpdateLeaveCurrentStation()
        {
            //current station will be null if the route hasn't entered the first station
            if (currentStationNode != null)
            {
                //leave the current station
                currentStationNode.CurrentStation.FlightLeftStation();
                currentStationNode.IsDone = true;
            }   
        }
        public IFlightRoute Clone()
        {         
            return (IFlightRoute)MemberwiseClone(); 
        }               
        public void UpdateCurrentStationIsDone()
        {
            currentStationNode.IsDone = true;
            if (currentStationNode.NextStationNodes.Count() == 0)
                IsDone = true;
        }
    }
}

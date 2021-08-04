import React from 'react';
import Loader from '../Components/Loader';
import Flights from '../Components/Flights';
import { useSignalR } from '../Hooks/ConnectionSignalR';
import Stations from '../Components/Stations';

function Home() {
  const url = `https://localhost:44347/api/airport`;

  let airportState = useSignalR(`${url}`);

  let landingsContent = null;
  let takeOffsContent = null;
  let stationsContent = null;

  if (airportState.error)
    landingsContent =
      takeOffsContent =
      stationsContent =
        (
          <div>
            <div className="bg-red-300 p-3">
              There was an error please refresh or try again later.
            </div>
          </div>
        );

  if (airportState.loading) {
    landingsContent = takeOffsContent = stationsContent = <Loader></Loader>;
  }

  if (airportState.data) {
    console.log(airportState.data);
    landingsContent = (
      <Flights flights={airportState.data.landings} isLanding={true} />
    );
    takeOffsContent = (
      <Flights flights={airportState.data.takeOffs} isLanding={false} />
    );
    stationsContent = <Stations stations={airportState.data.stations} />;
  }

  return (
    <div className="container">
      <div>
        <h1 className="font-bold">Stations:</h1>
        <div>{stationsContent}</div>
      </div>
      <div>
        <h1 className="font-bold">Landings:</h1>
        <div>{landingsContent}</div>
      </div>
      <div>
        <h1 className="font-bold">TakeOffs:</h1>
        <div>{takeOffsContent}</div>
      </div>
    </div>
  );
}
export default Home;

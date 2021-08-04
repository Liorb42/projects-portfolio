import React from 'react';
import StationCard from '../Components/StationCard';

function Stations(props) {
  let stations = props.stations.map((station) => (
    <StationCard key={station.id} station={station} />
  ));

  return (
    <table className="border table-fixed">
      <thead>
        <tr className="text-left bg-blue-200 flex-no-shrink w-full border-2">
          <th className="w-1/5 p-1">Station number</th>
          <th className="w-1/5 p-1">Flight number</th>
          <th className="w-1/5 p-1">Stay Duration</th>
          <th className="w-1/5 p-1">Start Time</th>
          <th className="w-1/5 p-1">Time Left</th>
        </tr>
      </thead>
      <tbody>{stations}</tbody>
    </table>
  );
}

export default Stations;

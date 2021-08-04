import React from 'react';
import FlightCard from '../Components/FlightCard';

function Flights(props) {
  let flights = props.flights.map((flight) => (
    <FlightCard key={flight.id} flight={flight} isLanding={props.isLanding} />
  ));
  let direction = (() => {
    if (props.isLanding) return 'Landing';
    else return 'TakeOff';
  })();

  return (
    <table className="border table-fixed">
      {
        <thead>
          <tr className="text-left bg-blue-200 flex-no-shrink w-full border-2">
            <th className="w-1/8 p-1">Flight number</th>
            <th className="w-1/8 p-1">From</th>
            <th className="w-1/8 p-1">To</th>
            <th className="w-5/8 p-1">{direction} Time</th>
          </tr>
        </thead>
      }
      <tbody>{flights}</tbody>
    </table>
  );
}

export default Flights;

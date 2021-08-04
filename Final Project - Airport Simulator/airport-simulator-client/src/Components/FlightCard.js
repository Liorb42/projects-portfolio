import React from 'react';
import TimeService from '../Services/TimeService';

function FlightCard(props) {
  let time;
  let color;
  if (props.isLanding) {
    time = TimeService.extractTimeFromString(props.flight.arrivalTime);
    color = 'green-200';
  } else {
    time = TimeService.extractTimeFromString(props.flight.departureTime);
    color = 'yellow-200';
  }
  let className = `bg-${color} flex-no-shrink w-full border-2`;

  return (
    <tr className={className}>
      <td className="p-3">
        <span>{props.flight.number}</span>
      </td>
      <td className="p-3">
        <span>{props.flight.origin}</span>
      </td>
      <td className="p-3">
        <span>{props.flight.destination}</span>
      </td>
      <td className="p-3">
        <span>{time}</span>
      </td>
    </tr>
  );
}

export default FlightCard;

import React, { useEffect, useState } from 'react';
import TimeService from '../Services/TimeService';

function StationCard(props) {
  let stayStartTime;
  if (props.station.stayStartTime === '0001-01-01T00:00:00')
    stayStartTime = '-';
  else
    stayStartTime = TimeService.extractTimeFromString(
      props.station.stayStartTime
    );
  let color = 'blue-100';
  if (props.station.isFlightLanding) color = 'green-200';
  if (props.station.isFlightTakingOff) color = 'yellow-200';

  let className = `bg-${color} flex-no-shrink w-full border-2`;

  const [timeLeft, setTimeLeft] = useState(
    TimeService.calculateTimeLeft(
      props.station.stayStartTime,
      props.station.totalDurationOfStay
    )
  );

  useEffect(() => {
    const timer = setTimeout(() => {
      setTimeLeft(
        TimeService.calculateTimeLeft(
          props.station.stayStartTime,
          props.station.totalDurationOfStay
        )
      );
    }, 1000);
    return () => clearTimeout(timer);
  });

  return (
    <tr className={className}>
      <td className="p-3">
        <span>{props.station.number}</span>
      </td>
      <td className="p-3">
        <span>{props.station.currentFlightId}</span>
      </td>
      <td className="p-3">
        <span>
          {TimeService.AddZero(props.station.totalDurationOfStay.minutes)}:
          {TimeService.AddZero(props.station.totalDurationOfStay.seconds)}
        </span>
      </td>
      <td className="p-3">
        <span>{stayStartTime}</span>
      </td>
      <td className="p-3">
        <span>{timeLeft}</span>
      </td>
    </tr>
  );
}

export default StationCard;

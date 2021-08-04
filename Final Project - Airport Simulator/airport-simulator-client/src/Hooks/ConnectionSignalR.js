import { HubConnectionBuilder } from '@microsoft/signalr';
import { useEffect, useState } from 'react';

export const useSignalR = (url) => {
  const [connection, setConnection] = useState(null);

  const [request, setRequest] = useState({
    loading: false,
    data: null,
    error: false,
  });

  useEffect(() => {
    setRequest({
      loading: true,
      data: null,
      error: false,
    });
    const connect = new HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => console.log('Connection started!'))
        .then(() =>
          connection.on('ReceiveAirportState', (state) => {
            setRequest({
              loading: false,
              data: state,
              error: false,
            });
          })
        )
        .catch((error) => {
          console.log('Error while establishing connection :(');
          setRequest({
            loading: false,
            data: null,
            error: true,
          });
        });
    }
  }, [connection]);

  return request;
};

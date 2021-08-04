const express = require('express');
const app = express();
const { getState, setState } = require('./state');
const config = require('./config');
const port = config.port;

const socket = require('socket.io');

const server = app.listen(port, () => {
  console.log(`server is working port ${port}!`);
});

setState(
  'io',
  socket(server, {
    cors: {
      origin: '*',
    },
  })
);

const registerConnectionHandlers = require('./eventHandlers/connectionHandlers');
const onConnection = (socket) => {
  registerConnectionHandlers(socket);
};

getState().io.on('connection', onConnection);

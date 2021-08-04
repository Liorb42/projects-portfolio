const express = require('express');
const app = express();
const cors = require('cors');

const { getState, setState } = require('./state');
const config = require('./config');
const port = config.port;
const bodyParser = require('body-parser');
const jwt = require('jsonwebtoken');

const logIn = require('./routes/logIn');
const register = require('./routes/register');
const { sendErrorStatus, set404Status } = require('./controllers/errorHandler');

const server = app.listen(port, () => {
  console.log(`App listening on port ${port}!`);
});

const socket = require('socket.io');
const registerConnectionHandlers = require('./eventHandlers/connectionHandler');
const registerLobbyHandlers = require('./eventHandlers/lobbyHandler');
const registerGameHandlers = require('./eventHandlers/gameHandler');

const onConnection = (socket) => {
  registerConnectionHandlers(socket);
  registerLobbyHandlers(socket);
  registerGameHandlers(socket);
};

app.all('/*', function (req, res, next) {
  res.header('Access-Control-Allow-Origin', '*');
  res.header(
    'Access-Control-Allow-Headers',
    'Content-Type,X-Requested-With,cache-control,pragma'
  );
  res.setHeader(
    'Access-Control-Allow-Methods',
    'GET, POST, OPTIONS, PUT, PATCH, DELETE'
  );
  res.setHeader('Access-Control-Allow-Credentials', true);

  next();
});
app.use(cors());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use('/logIn', logIn);
app.use('/register', register);

app.use('/*', set404Status);
app.use(sendErrorStatus);

setState(
  'io',
  socket(server, {
    cors: {
      origin: '*',
    },
  })
);
setState('jwt', jwt);

getState().io.on('connection', onConnection);

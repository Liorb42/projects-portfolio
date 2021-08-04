const { validateToken } = require('../services/securityService');
const {
  removePlayerFromArray,
  getPlayerById,
  CancelOnGoingGame,
} = require('../services/xOgameService');
const {
  mapSocketPlayer,
  removeSocketPlayerMap,
} = require('../services/socketMapService');
const {
  EmitGameEnded,
  EmitLoggedInLobbyChangedEvent,
} = require('./emitEventHandler');

module.exports = (socket) => {
  const disconnect = (reason) => {
    console.log(`A user has disconnected. ${socket.id}`);

    const playerId = removeSocketPlayerMap(socket);
    const player = getPlayerById(playerId);
    removePlayerFromArray(player);
    const game = CancelOnGoingGame(player);
    if (game) EmitGameEnded(game);
    EmitLoggedInLobbyChangedEvent();
  };
  const connect = () => {
    console.log(`A user just connected. ${socket.id}`);

    if (validateToken(socket)) {
      //map this socket instance to the player
      mapSocketPlayer([Number.parseInt(socket.decoded), socket]);
    } else socket.disconnect();
  };
  socket.on('disconnect', disconnect);
  connect();
};

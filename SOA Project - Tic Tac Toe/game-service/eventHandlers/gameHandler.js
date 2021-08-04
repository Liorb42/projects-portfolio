const { getState } = require('../state');
const { logError } = require('../services/loggerService');

const {
  createNewGame,
  addGameId,
  getGameByPlayerId,
  getPlayerSymbol,
  isMoveValid,
  move,
  switchTurn,
  isGameWon,
  endGame,
  getPlayerById,
} = require('../services/xOgameService');
const { getSocketByPlayerId } = require('../services/socketMapService');
const {
  EmitGameRequestEvent,
  EmitGameRequestDeniedEvent,
  EmitNewGameEvent,
  EmitGameWonEvent,
  EmitMoveEvent,
  EmitGameEndedEvent,
  EmitGameRequestCanceledEvent,
  EmitGetUpdatedPlayerEvent,
} = require('./emitEventHandler');

module.exports = (socket) => {
  const onGameRequest = (args) => {
    const offeringId = socket.decoded;
    let receivingId = 0;
    try {
      receivingId = args[0];
      const receivingSocket = getSocketByPlayerId(receivingId);
      EmitGameRequestEvent(receivingSocket, offeringId);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onGameRequestDenied = (args) => {
    const refusingId = socket.decoded;
    let receivingId = 0;
    try {
      receivingId = args[0];
      const receivingSocket = getSocketByPlayerId(receivingId);
      EmitGameRequestDeniedEvent(receivingSocket, refusingId);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onGameCanceled = (args) => {
    const cancelingId = socket.decoded;
    let receivingId = 0;
    try {
      receivingId = args[0];
      const receivingSocket = getSocketByPlayerId(receivingId);
      EmitGameRequestCanceledEvent(receivingSocket, cancelingId);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onCreateNewGame = (args) => {
    let xId = 0;
    let oId = 0;
    try {
      xId = args[0];
      oId = args[1];

      const game = createNewGame(xId, oId);

      createNewRoom(xId, oId, game.id);

      //add game id to players profile
      addGameId(xId, oId, game.id);

      EmitNewGameEvent(game.id, game);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const createNewRoom = (xId, oId, gameId) => {
    try {
      const socketX = getSocketByPlayerId(xId);
      const socketO = getSocketByPlayerId(oId);
      socketX.join(`${gameId}`);
      socketO.join(`${gameId}`);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const leaveRoom = (xId, oId, gameId) => {
    try {
      const socketX = getSocketByPlayerId(xId);
      const socketO = getSocketByPlayerId(oId);
      socketX.leave(`${gameId}`);
      socketO.leave(`${gameId}`);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onMakeMove = (args) => {
    let playerId;
    let rowInd;
    let colInx;
    let playerSymbol;

    try {
      playerId = args[0];
      rowInd = args[1];
      colInx = args[2];

      const game = getGameByPlayerId(playerId);
      if (game) {
        playerSymbol = getPlayerSymbol(playerId, game);
        if (isMoveValid(game, rowInd, colInx)) {
          move(game, playerSymbol, rowInd, colInx);
          game.isMoveValid = true;
          switchTurn(game);
        }
        if (isGameWon(game)) {
          game.isGameWon = true;
          game.winnerId = playerId;
          endGame(game);
          EmitGameWonEvent(game.id, game);
          //leave the room of the game
          leaveRoom(game.playerXId, game.playerOId, game.id);
        }
        EmitMoveEvent(game.id, game);
      }
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onEndGame = (args) => {
    const games = getState().currentXoGames;
    let game;
    try {
      let gameId = args[0];
      for (const g of games) {
        if (g.id == gameId) game = g;
      }
      endGame(game);
      EmitGameEndedEvent(game);
      leaveRoom(game.playerXId, game.playerOId, game.id);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onGetUpdatedPlayer = (args) => {
    try {
      let playerId = args[0];
      let player = getPlayerById(playerId);
      const receivingSocket = getSocketByPlayerId(playerId);

      EmitGetUpdatedPlayerEvent(receivingSocket, player);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };

  socket.on('gameRequest', onGameRequest);
  socket.on('gameRequestDenied', onGameRequestDenied);
  socket.on('cancelGameRequest', onGameCanceled);

  socket.on('startNewGame', onCreateNewGame);
  socket.on('makeMove', onMakeMove);
  socket.on('endGame', onEndGame);
  socket.on('getUpdatedPlayer', onGetUpdatedPlayer);
};

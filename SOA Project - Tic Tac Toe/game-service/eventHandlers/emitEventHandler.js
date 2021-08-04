const { getState } = require('../state');

module.exports = {
  EmitLoggedInLobbyChangedEvent() {
    getState().io.emit(
      'LoggedInLobbyChangedEvent',
      JSON.parse(JSON.stringify(getState().loggedInPlayers))
    );
  },
  EmitGameRequestEvent(recieveSocket, offerReqId) {
    getState().io.to(recieveSocket?.id).emit('gameRequestEvent', offerReqId);
  },
  EmitGameRequestDeniedEvent(recieveSocket, refusingId) {
    getState()
      .io.to(recieveSocket?.id)
      .emit('gameRequestDeniedEvent', refusingId);
  },
  EmitGameRequestCanceledEvent(recieveSocket, cancelingId) {
    getState()
      .io.to(recieveSocket?.id)
      .emit('gameRequestCanceledEvent', cancelingId);
  },
  EmitNewGameEvent(gameId, game) {
    getState().io.to(`${gameId}`).emit('newGameEvent', game);
    getState().io.emit(
      'LoggedInLobbyChangedEvent',
      JSON.parse(JSON.stringify(getState().loggedInPlayers))
    );
  },
  EmitGameWonEvent(gameId, game) {
    getState().io.to(`${gameId}`).emit('gameWonEvent', game);
    getState().io.emit(
      'LoggedInLobbyChangedEvent',
      JSON.parse(JSON.stringify(getState().loggedInPlayers))
    );
  },
  EmitMoveEvent(gameId, game) {
    getState().io.to(`${gameId}`).emit('moveEvent', game);
  },
  EmitGameEndedEvent(game) {
    getState().io.to(`${game.id}`).emit('GameEndedEvent', game);
    getState().io.emit(
      'LoggedInLobbyChangedEvent',
      JSON.parse(JSON.stringify(getState().loggedInPlayers))
    );
  },
  EmitgameRequestCanceledEvent(recieveSocket, cancelingId) {
    getState()
      .io.to(recieveSocket?.id)
      .emit('gameRequestCanceledEvent', cancelingId);
    getState().io.emit(
      'LoggedInLobbyChangedEvent',
      JSON.parse(JSON.stringify(getState().loggedInPlayers))
    );
  },
  EmitGetUpdatedPlayerEvent(recieveSocket, player) {
    getState().io.to(recieveSocket?.id).emit('getUpdatedPlayerEvent', player);
  },
};

const { getState } = require('../state');

module.exports = {
  mapSocketPlayer(args) {
    //map a socket instance to a player

    let playerId;
    let socket;

    try {
      playerId = args[0];
      socket = args[1];
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
    if (playerId && socket) getState().socketPlayersMap.set(playerId, socket);
  },
  removeSocketPlayerMap(socket) {
    //delete from map the socket instance and player

    let key;
    let map = getState().socketPlayersMap;
    map.forEach((mapSocket, playerId) => {
      if (socket === mapSocket) {
        key = playerId;
      }
    });
    map.delete(key);
    return key;
  },
  getSocketByPlayerId(playerId) {
    const map = getState().socketPlayersMap;
    try {
      return map.get(playerId);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  },
};

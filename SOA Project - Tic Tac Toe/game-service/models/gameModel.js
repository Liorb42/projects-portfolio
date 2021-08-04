const { getState, setState } = require('../state');

const chooseStartingPlayer = (playerXId, playerOId) => {
  let toss = Math.floor(Math.random() * 2) === 0;
  if (toss) {
    return playerXId;
  } else {
    return playerOId;
  }
};
module.exports = class Game {
  constructor(playerXId, playerOId) {
    this.id = getState().currentXoGames.values.length + 1;
    this.playerXId = playerXId;
    this.playerOId = playerOId;
    this.gameMtx = [
      ['&nbsp', '&nbsp', '&nbsp'],
      ['&nbsp', '&nbsp', '&nbsp'],
      ['&nbsp', '&nbsp', '&nbsp'],
    ];
    this.playerTurn = chooseStartingPlayer(this.playerXId, this.playerOId);
    this.isGameWon = false;
    this.winnerId = 0;
    this.isMoveValid = false;
  }
};

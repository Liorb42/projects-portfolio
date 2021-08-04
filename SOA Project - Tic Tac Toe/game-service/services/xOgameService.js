const { getState, setState } = require('../state');
const Player = require('../models/playerModel');
const Game = require('../models/gameModel');

module.exports = {
  createNewPlayer(user) {
    let player = new Player(user);
    let players = getState().loggedInPlayers;
    let playerExist = false;

    //check if the player is already on the list
    players.forEach((p) => {
      if (p.id === player.id) playerExist = true;
    });
    if (!playerExist) {
      players.push(player);
      setState('loggedInPlayers', players);
      return player;
    } else return null;
  },
  removePlayerFromArray(player) {
    const loggedInPlayers = getState().loggedInPlayers;
    loggedInPlayers.forEach((p) => {
      if (p.id === player.id) {
        const index = loggedInPlayers.indexOf(player);
        if (index > -1) loggedInPlayers.splice(index, 1);
      }
    });
    setState('loggedInPlayers', loggedInPlayers);
  },
  CancelOnGoingGame(player) {
    const games = getState().currentXoGames;
    let game = null;
    //check it the player is part on a current game
    games.forEach((g) => {
      if (g.playerXId === player.id || g.playerOId === player.id) {
        module.exports.endGame(g);
        game = g;
      }
    });
    setState('currentXoGames', games);
    return game;
  },
  getPlayerById(id) {
    const players = getState().loggedInPlayers;
    return players.find((p) => p.id === id);
  },
  getPlayerSymbol(playerId, game) {
    if (game.playerXId === playerId) return 'x';
    else if (game.playerOId === playerId) return 'o';
    else return '&nbsp';
  },
  createNewGame(xId, oId) {
    const game = new Game(xId, oId);
    let games = getState().currentXoGames;
    games.add(game);
    setState('currentXoGames', games);
    return game;
  },
  addGameId(xId, oId, gameId) {
    const players = getState().loggedInPlayers;
    for (const p of players) {
      if (p.id === xId || p.id === oId) {
        p.currentGameId = gameId;
        p.isPlaying = true;
      }
    }
    setState('loggedInPlayers', players);
  },
  getGameByPlayerId(id) {
    const games = getState().currentXoGames;
    let game;
    for (const g of games) {
      if (g.playerXId == id || g.playerOId == id) game = g;
    }
    return game;
  },
  isMoveValid(game, rowInd, colInx) {
    if (game) {
      if (game.gameMtx[rowInd][colInx] === '&nbsp') return true;
    }
    return false;
  },
  move(game, playerSymbol, rowInd, colInx) {
    if (game && playerSymbol) {
      game.gameMtx[rowInd][colInx] = playerSymbol;
    }
  },
  switchTurn(game) {
    game.playerTurn =
      game.playerTurn === game.playerXId ? game.playerOId : game.playerXId;
  },
  isGameWon(game) {
    //check rows
    for (let index = 0; index < 3; index++) {
      if (
        game.gameMtx[index][0] &&
        game.gameMtx[index][0] !== '&nbsp' &&
        game.gameMtx[index][0] === game.gameMtx[index][1] &&
        game.gameMtx[index][1] === game.gameMtx[index][2]
      )
        return true;
    }

    //check columns
    for (let index = 0; index < 3; index++) {
      if (
        game.gameMtx[0][index] &&
        game.gameMtx[0][index] !== '&nbsp' &&
        game.gameMtx[0][index] === game.gameMtx[1][index] &&
        game.gameMtx[1][index] === game.gameMtx[2][index]
      )
        return true;
    }

    //check diagonal
    if (
      game.gameMtx[0][0] &&
      game.gameMtx[0][0] !== '&nbsp' &&
      game.gameMtx[0][0] === game.gameMtx[1][1] &&
      game.gameMtx[1][1] === game.gameMtx[2][2]
    )
      return true;

    //check reverse diagonal
    if (
      game.gameMtx[0][2] &&
      game.gameMtx[0][2] !== '&nbsp' &&
      game.gameMtx[0][2] === game.gameMtx[1][1] &&
      game.gameMtx[1][1] === game.gameMtx[2][0]
    )
      return true;

    return false;
  },
  endGame(game) {
    // if there is a winner - increse win count
    if (game && game.winnerId) {
      getState().loggedInPlayers.find(
        (p) => p.id === game.winnerId
      ).totalWins += 1;
    }
    // reset game id for players
    const playerX = module.exports.getPlayerById(game.playerXId);
    if (playerX) {
      playerX.currentGameId = 0;
      playerX.isPlaying = false;
    }

    const playerO = module.exports.getPlayerById(game.playerOId);
    if (playerO) {
      playerO.currentGameId = 0;
      playerO.isPlaying = false;
    }

    // remove game from state
    getState().currentXoGames.delete(game);
  },
};

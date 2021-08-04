let state = {
  currentXoGames: new Set(),
  loggedInPlayers: new Array(),
  socketPlayersMap: new Map(),
  io: null,
  jwt: null,
  jwtSecret: '6DA97F41BA841F2BCEB07F9FDC1E9F68002EACB2AEACB157B7BA88B4AA00693F',
};

const getState = () => state;

const setState = (prop, value) => {
  state[prop] = value;
};

module.exports = {
  getState,
  setState,
};

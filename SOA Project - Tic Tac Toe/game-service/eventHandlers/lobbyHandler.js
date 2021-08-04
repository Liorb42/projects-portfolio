const { EmitLoggedInLobbyChangedEvent } = require('./emitEventHandler');

module.exports = (socket) => {
  const onLogInLobby = (socket) => {
    //emit to all plugged in players the current players list
    EmitLoggedInLobbyChangedEvent();
  };
  const onLogOutLobby = () => {
    socket.disconnect();
  };

  socket.on('logInLobby', onLogInLobby);
  socket.on('logOutLobby', onLogOutLobby);
};

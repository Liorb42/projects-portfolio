const { getState } = require('../state');
const { logError } = require('../services/loggerService');

module.exports = (socket) => {
  const connect = () => {
    console.log(`A user just connected. ${socket.id}`);
  };
  const disconnect = (reason) => {
    console.log(`A user has disconnected. ${socket.id}`);
  };
  const onJoinRoom = (arg) => {
    let roomName;
    try {
      roomName = arg[0];
      socket.join(roomName);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onSendMsgToRoom = (arg) => {
    let roomName;
    let senderName;
    let mgs;

    try {
      roomName = arg[0];
      senderName = arg[1];
      mgs = arg[2];
      const io = getState().io;
      io.to(roomName).emit('newMsgEvent', [senderName, mgs]);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };
  const onLeaveRoom = (arg) => {
    let roomName;
    try {
      roomName = arg[0];
      socket.leave(roomName);
    } catch (err) {
      logError(`Error Message : ${err}`);
    }
  };

  socket.on('disconnect', disconnect);
  socket.on('joinRoom', onJoinRoom);
  socket.on('sendMsgToRoom', onSendMsgToRoom);
  socket.on('leaveRoom', onLeaveRoom);
  connect();
};

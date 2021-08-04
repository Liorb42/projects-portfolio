const { getState, setState } = require('../state');

module.exports = {
  generateAccessToken(payload) {
    let jwt = getState().jwt;
    let token = jwt.sign(
      JSON.parse(JSON.stringify(payload)),
      getState().jwtSecret
    );
    setState('jwt', jwt);
    return token;
  },
  validateToken(socket) {
    let isValid;
    if (socket.handshake.query && socket.handshake.query.token) {
      getState().jwt.verify(
        socket.handshake.query.token,
        getState().jwtSecret,
        (err, decoded) => {
          if (err) {
            socket.on('error', () => {
              console.log(`Validation failed ${socket.id}`);
              logError(`Error Message : ${err}`);
            });
            socket.disconnect();
            isValid = false;
          } else {
            //add the player id to the socket instance
            socket.decoded = decoded;
            console.log(`validated user. ${socket.id}`);
            isValid = true;
          }
        }
      );
    } else {
      socket.on('disconnect', () => {
        console.log(`Invalid token ${socket.id}`);
      });
      socket.disconnect();
      isValid = false;
    }
    return isValid;
  },
};

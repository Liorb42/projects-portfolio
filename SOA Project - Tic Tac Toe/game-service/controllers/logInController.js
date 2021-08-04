const { createNewPlayer } = require('../services/xOgameService');
const { generateAccessToken } = require('../services/securityService');
const { getUserFromDb } = require('../services/contextService');

module.exports = {
  async authenticateUserAsync(req, res, next) {
    if (req.body.name && req.body.password) {
      let user = await getUserFromDb(req.body.name, req.body.password);
      user
        ? (res.locals.user = user)
        : res.status(401).send({
            message: 'Incorrect name or password!',
          });

      next();
    } else {
      res.status(401).send({
        message: 'Invalid input for name or password',
      });
    }
  },
  createPlayer(req, res, next) {
    let user = res.locals.user;
    let player;
    let token;
    if (user) {
      player = createNewPlayer(user);
      if (player) {
        token = generateAccessToken(player?.id);
        res.send({ jwt: token, player: player });
      } else {
        res.status(401).send({
          message: 'player is already logged in',
        });
      }
    }
  },
};

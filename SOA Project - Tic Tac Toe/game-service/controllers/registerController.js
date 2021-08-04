const { registerUser } = require('../services/contextService');

module.exports = {
  validateInput(req, res, next) {
    if (
      req.body.name &&
      req.body.email &&
      req.body.password &&
      req.body.name?.length >= 4 &&
      /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$/.test(req.body.email) &&
      /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(req.body.password)
    )
      next();
    else {
      res.send({ message: `Invalid input` });
    }
  },
  async registerUserAsync(req, res, next) {
    if ((req.body.name, req.body.email, req.body.password)) {
      let message = await registerUser(
        req.body.name,
        req.body.email,
        req.body.password
      );
      console.log(message);
      message ? res.send({ message: `${message}` }) : next();
    }
  },
};

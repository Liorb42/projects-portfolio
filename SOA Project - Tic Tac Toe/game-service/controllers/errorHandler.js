const { logError } = require('../services/loggerService');

module.exports = {
  errorHandler: (fn) => (req, res, next) => {
    Promise.resolve(fn(req, res, next)).catch(next);
  },
  sendErrorStatus(err, req, res, next) {
    logError(`Error Message : ${err}`);
    res.sendStatus(err.status || 500);
  },
  set404Status(req, res, next) {
    const err = new Error('Not Found');
    err.status = 404;
    next(err);
  },
};

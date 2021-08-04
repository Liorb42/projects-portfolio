const express = require('express');
const router = express.Router({ mergeParams: true });
const {
  authenticateUserAsync,
  createPlayer,
} = require('../controllers/logInController');
const { errorHandler } = require('../controllers/errorHandler');

router.post(
  '/',
  errorHandler(authenticateUserAsync),
  errorHandler(createPlayer)
);
module.exports = router;

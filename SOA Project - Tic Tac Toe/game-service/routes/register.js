const express = require('express');
const router = express.Router({ mergeParams: true });
const {
  validateInput,
  registerUserAsync,
} = require('../controllers/registerController');
const { errorHandler } = require('../controllers/errorHandler');

router.put('/', errorHandler(validateInput), errorHandler(registerUserAsync));
module.exports = router;

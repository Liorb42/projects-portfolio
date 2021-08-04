let state = {
  io: null,
};

const getState = () => state;

const setState = (prop, value) => {
  state[prop] = value;
};

module.exports = {
  getState,
  setState,
};

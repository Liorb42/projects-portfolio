stringToHash = (string) => {
  var hash = 0;
  if (string.length == 0) return hash;
  for (i = 0; i < string.length; i++) {
    char = string.charCodeAt(i);
    hash = (hash << 5) - hash + char;
    hash = hash & hash;
  }
  return hash;
};

class Player {
  constructor(user) {
    this.id = stringToHash(user.email);
    this.name = user.name;
    this.totalWins = 0;
    this.isPlaying = false;
    this.currentGameId = null;
  }
}

module.exports = Player;

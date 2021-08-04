import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { GameModel } from 'src/app/models/game.model';
import { PlayerModel } from 'src/app/models/player.model';
import { GameService } from 'src/app/services/game.service';
import { PlayerInfoService } from 'src/app/services/player-info.service';

@Component({
  selector: 'app-xo-game',
  templateUrl: './xo-game.component.html',
  styleUrls: ['./xo-game.component.css'],
})
export class XoGameComponent implements OnInit, OnDestroy {
  currentGame: GameModel;
  player: PlayerModel;
  playingSymbol: String;
  isPlayerTurn: Boolean;
  isPlayerWon: Boolean;
  messageText: String;
  bottonText: String;
  isGameEnded: Boolean;
  subscription: Subscription;

  constructor(
    private game: GameService,
    private router: Router,
    private playerInfo: PlayerInfoService
  ) {
    this.currentGame = JSON.parse(sessionStorage.getItem('game'));
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.player = player)
    );
    this.setPlayingSymbol();
    this.setTurn();
  }

  ngOnInit(): void {
    this.game.listenToEvent('moveEvent', (data) => {
      this.displayMove(data);
    });
    this.game.listenToEvent('gameWonEvent', (data) => {
      this.gameWon(data);
    });
    this.game.listenToEvent('GameEndedEvent', (data) => {
      this.gameEnded(data);
    });
    this.game.listenToEvent('getUpdatedPlayerEvent', (data) => {
      this.updatePlayer(data);
    });
    this.isGameEnded = false;
    this.bottonText = 'End Game';
  }
  ngOnDestroy(): void {
    sessionStorage.setItem('game', JSON.stringify(''));
    this.game.emitEvent('endGame', [this.currentGame.id]);
    this.subscription.unsubscribe();
  }
  setPlayingSymbol() {
    this.player.id === this.currentGame.playerXId
      ? (this.playingSymbol = 'x')
      : (this.playingSymbol = 'o');
  }
  setTurn() {
    this.isPlayerTurn = this.player.id === this.currentGame.playerTurn;
    this.messageText = this.isPlayerTurn
      ? 'Its your turn'
      : 'Please wait your turn';
  }
  makeMove(row: Number, column: Number) {
    if (this.isPlayerTurn)
      this.game.emitEvent('makeMove', [this.player.id, row, column]);
  }
  displayMove(game: GameModel) {
    this.currentGame = game;
    this.setTurn();
  }
  gameWon(game: GameModel) {
    this.currentGame = game;
    if (this.currentGame.winnerId === this.player.id) {
      this.isPlayerWon = true;
      this.game.emitEvent('getUpdatedPlayer', [this.player.id]);
    }
    this.messageText = this.isPlayerWon ? 'You Won!' : 'You Lost!';
    this.bottonText = 'Back to Lobby';
  }
  endGame() {
    this.game.emitEvent('endGame', [this.currentGame.id]);
    this.NavToLobby();
  }
  gameEnded(game: GameModel) {
    this.isGameEnded = true;
    this.bottonText = 'Back to Lobby';
  }
  updatePlayer(player: PlayerModel) {
    this.playerInfo.changePlayer(player);
  }

  NavToLobby() {
    this.router.navigate(['/lobby']);
  }
}

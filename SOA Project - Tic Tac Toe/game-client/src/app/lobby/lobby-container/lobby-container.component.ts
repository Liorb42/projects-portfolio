import { Component, OnDestroy, OnInit, PlatformRef } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { PlayerModel } from 'src/app/models/player.model';
import { GameService } from 'src/app/services/game.service';
import { PlayerInfoService } from 'src/app/services/player-info.service';

type requestTuple = [Number, String];

@Component({
  selector: 'app-lobby-container',
  templateUrl: './lobby-container.component.html',
  styleUrls: ['./lobby-container.component.css'],
})
export class LobbyContainerComponent implements OnInit, OnDestroy {
  currentPlayer: PlayerModel;
  roomName: String;
  subscription: Subscription;

  private _playersInLobby: PlayerModel[];
  set playersInLobby(value: PlayerModel[]) {
    this._playersInLobby = value;
  }
  get playersInLobby(): PlayerModel[] {
    return this._playersInLobby;
  }

  private _sentRequests: Number[];
  set sentRequests(value: Number[]) {
    this._sentRequests = value;
  }
  get sentRequests(): Number[] {
    return this._sentRequests;
  }

  private _recievedRequests: requestTuple[];
  set recievedRequests(value: requestTuple[]) {
    this._recievedRequests = value;
  }
  get recievedRequests(): requestTuple[] {
    return this._recievedRequests;
  }

  constructor(
    private game: GameService,
    private router: Router,
    private playerInfo: PlayerInfoService
  ) {}

  ngOnInit(): void {
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.currentPlayer = player)
    );
    this.roomName = 'lobby';

    this.registerEventListeners();
    this.sentRequests = new Array();
    this.recievedRequests = new Array();

    this.game.emitEvent('logInLobby');
  }

  ngOnDestroy(): void {
    this.sentRequests.forEach((req) => {
      this.cancelGameRequest(req);
    });
    this.recievedRequests.forEach((req) => {
      this.denyGameRequest(req[0]);
    });
    this.subscription.unsubscribe();
  }

  private registerEventListeners() {
    this.game.listenToEvent('LoggedInLobbyChangedEvent', (data) => {
      sessionStorage.setItem('playersInLobby', JSON.stringify(data));
      this.playersInLobby = <PlayerModel[]>data;
    });
    this.game.listenToEvent('gameRequestEvent', (data) => {
      this.showGameRequest(data);
    });
    this.game.listenToEvent('gameRequestDeniedEvent', (data) => {
      this.gameRequestCanceled(data);
    });
    this.game.listenToEvent('gameRequestCanceledEvent', (data) => {
      this.gameRequestCanceled(data);
    });
    this.game.listenToEvent('newGameEvent', (data) => {
      this.startNewGame(data);
    });
  }

  requestGame(playerId: Number) {
    const tmp = this.sentRequests;
    tmp.push(playerId);
    this.sentRequests = tmp;
    this.game.emitEvent('gameRequest', [playerId]);
  }
  cancelGameRequest(playerId: Number) {
    this.game.emitEvent('cancelGameRequest', [playerId]);
    this.removeRequestFromSent(playerId);
  }
  showGameRequest(data) {
    const id = Number.parseInt(data);
    const tmp = this.recievedRequests;
    tmp.push([id, this.getPlayerName(id)]);
    this.recievedRequests = tmp;
  }
  acceptGameRequest(playerId: Number) {
    this.game.emitEvent('startNewGame', [this.currentPlayer.id, playerId]);
    this.sentRequests = [];
    this.recievedRequests = [];
  }
  denyGameRequest(playerId: Number) {
    this.game.emitEvent('gameRequestDenied', [playerId]);
    this.removeRequestFromRecieved(playerId);
  }
  gameRequestCanceled(data) {
    const playerId = Number.parseInt(data);
    this.removeRequestFromRecieved(playerId);
    this.removeRequestFromSent(playerId);
  }
  startNewGame(game) {
    sessionStorage.setItem('game', JSON.stringify(game));
    this.router.navigate(['/game']);
  }
  getPlayerName(playerId: Number) {
    return this.playersInLobby.find((p) => p.id === playerId).name;
  }
  private removeRequestFromSent(playerId: Number) {
    const tmp = this.sentRequests;
    if (playerId) {
      const index = tmp.indexOf(playerId);
      if (index > -1) {
        tmp.splice(index, 1);
      }
    }
    this.sentRequests = tmp;
  }

  private removeRequestFromRecieved(playerId: Number) {
    const tmp = this.recievedRequests;

    if (playerId) {
      const index = tmp.indexOf(tmp.find((tuple) => tuple[0] === playerId));
      if (index > -1) {
        tmp.splice(index, 1);
      }
    }
    this.recievedRequests = tmp;
  }
}

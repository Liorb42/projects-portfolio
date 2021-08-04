import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Subscription } from 'rxjs';
import { PlayerModel } from 'src/app/models/player.model';
import { PlayerInfoService } from 'src/app/services/player-info.service';

@Component({
  selector: 'app-connected-players',
  templateUrl: './connected-players.component.html',
  styleUrls: ['./connected-players.component.css'],
})
export class ConnectedPlayersComponent implements OnInit, OnDestroy {
  @Input()
  connectedPlayers: PlayerModel[];

  private _sentRequests: Number[];
  @Input() set sentRequests(value: Number[]) {
    this._sentRequests = value;
  }
  get sentRequests(): Number[] {
    return this._sentRequests;
  }

  currentPlayer: PlayerModel;
  otherPlayerId: Number;
  subscription: Subscription;

  @Output()
  onGameRequest: EventEmitter<Number> = new EventEmitter<Number>();
  @Output()
  onCancelGameRequest: EventEmitter<Number> = new EventEmitter<Number>();

  constructor(private playerInfo: PlayerInfoService) {}

  ngOnInit(): void {
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.currentPlayer = player)
    );
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  requestGame(otherPlayerid: Number) {
    this.onGameRequest.emit(otherPlayerid);
  }
  cancelGameRequest(otherPlayerid: Number) {
    this.onCancelGameRequest.emit(otherPlayerid);
  }

  isRequestSent(playerId: Number): Boolean {
    return this.sentRequests.indexOf(playerId) != -1;
  }
}

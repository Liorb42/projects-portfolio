import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { PlayerModel } from 'src/app/models/player.model';
type requestTuple = [Number, String];

@Component({
  selector: 'app-game-request',
  templateUrl: './game-request.component.html',
  styleUrls: ['./game-request.component.css'],
})
export class GameRequestComponent implements OnInit {
  private _recievedRequests: requestTuple[];
  @Input() set recievedRequests(value: requestTuple[]) {
    this._recievedRequests = value;
  }
  get recievedRequests(): requestTuple[] {
    return this._recievedRequests;
  }

  @Output()
  OnAcceptGameRequest: EventEmitter<Number> = new EventEmitter<Number>();
  @Output()
  OnDenyGameRequest: EventEmitter<Number> = new EventEmitter<Number>();

  constructor() {}

  ngOnInit(): void {}

  respondeToGameRequest(isAccepted: Boolean, playerId: Number) {
    isAccepted
      ? this.OnAcceptGameRequest.emit(playerId)
      : this.OnDenyGameRequest.emit(playerId);
  }
}

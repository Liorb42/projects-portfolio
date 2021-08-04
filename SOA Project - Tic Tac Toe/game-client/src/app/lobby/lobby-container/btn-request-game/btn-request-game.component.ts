import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-btn-request-game',
  templateUrl: './btn-request-game.component.html',
  styleUrls: ['./btn-request-game.component.css'],
})
export class BtnRequestGameComponent implements OnInit {
  private _isDisabled: Boolean;
  @Input() set isDisabled(value: Boolean) {
    this._isDisabled = value;
    this.setButtonText();
  }
  get isDisabled(): Boolean {
    return this._isDisabled;
  }

  private _isRequestSent: Boolean;
  @Input() set isRequestSent(value: Boolean) {
    this._isRequestSent = value;
    this.setButtonText();
  }

  get isRequestSent(): Boolean {
    return this._isRequestSent;
  }
  @Input()
  otherPlayerId: Number;

  @Output()
  onGameRequest: EventEmitter<Number> = new EventEmitter<Number>();

  @Output()
  onCancelGameRequest: EventEmitter<Number> = new EventEmitter<Number>();
  bottonText: String;

  constructor() {}

  ngOnInit(): void {}

  toggleGamerequest() {
    this.toggleBtn();
    this.isRequestSent
      ? this.onGameRequest.emit(this.otherPlayerId)
      : this.onCancelGameRequest.emit(this.otherPlayerId);
  }
  toggleBtn() {
    this.isRequestSent = !this.isRequestSent;
  }
  private setButtonText() {
    this.bottonText = this._isRequestSent ? 'Cancel request' : 'Invite to play';
    if (this._isDisabled) this.bottonText = 'Unavailable';
  }
}

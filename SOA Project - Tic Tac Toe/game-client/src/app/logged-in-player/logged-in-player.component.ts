import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PlayerModel } from '../models/player.model';
import { PlayerInfoService } from '../services/player-info.service';

@Component({
  selector: 'app-logged-in-player',
  templateUrl: './logged-in-player.component.html',
  styleUrls: ['./logged-in-player.component.css'],
})
export class LoggedInPlayerComponent implements OnInit, OnDestroy {
  private _player: PlayerModel;
  set player(value: PlayerModel) {
    this._player = value;
    this.setViewText();
  }
  get player(): PlayerModel {
    return this._player;
  }
  subscription: Subscription;
  msgText: String;
  numOfWins: Number;
  isWinsVisisble: Boolean = false;

  constructor(private playerInfo: PlayerInfoService) {}

  ngOnInit(): void {
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.player = player)
    );
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  setViewText() {
    if (this.player.name.length > 0) {
      this.msgText = `Hello ${this.player.name}`;
      this.numOfWins = this.player.totalWins;
      this.isWinsVisisble = true;
    } else {
      this.msgText = 'Please Login';
      this.isWinsVisisble = false;
      this.numOfWins = null;
    }
  }
}

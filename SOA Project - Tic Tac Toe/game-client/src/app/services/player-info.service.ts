import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { PlayerModel } from '../models/player.model';

@Injectable({
  providedIn: 'root',
})
export class PlayerInfoService {
  private playerSource = new BehaviorSubject(new PlayerModel());
  currentPlayer = this.playerSource.asObservable();

  constructor() {}

  changePlayer(player: PlayerModel) {
    this.playerSource.next(player);
  }
}

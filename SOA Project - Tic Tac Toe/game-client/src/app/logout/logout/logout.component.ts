import { Component, OnInit } from '@angular/core';
import { PlayerModel } from 'src/app/models/player.model';
import { GameService } from 'src/app/services/game.service';
import { PlayerInfoService } from 'src/app/services/player-info.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  message: string;

  constructor(
    private game: GameService,
    private playerInfo: PlayerInfoService
  ) {}

  ngOnInit(): void {
    this.game.emitEvent('logOutLobby');
    this.game.disconnect();
    this.playerInfo.changePlayer(new PlayerModel());
    sessionStorage.setItem('game', '');
    sessionStorage.setItem('token', '');

    this.message = 'you have logged out successfuly';
  }
}

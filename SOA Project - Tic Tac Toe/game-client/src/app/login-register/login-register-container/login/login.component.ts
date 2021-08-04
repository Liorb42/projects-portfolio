import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LogInResModel } from 'src/app/models/login-response.model';
import { GameHttpService } from '../../../services/context.service';
import { PlayerInfoService } from '../../../services/player-info.service';
import { Subscription } from 'rxjs';
import { PlayerModel } from 'src/app/models/player.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LogInComponent implements OnInit, OnDestroy {
  response: LogInResModel;
  errorMsg: String;
  player: PlayerModel;
  subscription: Subscription;

  constructor(
    private httpService: GameHttpService,
    private router: Router,
    private playerInfo: PlayerInfoService
  ) {}

  ngOnInit(): void {
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.player = player)
    );
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  login(name: string, password: string) {
    if (name && password) {
      this.httpService.loginPlayer(name, password).subscribe(
        (res) => {
          this.response = res;
          this.playerInfo.changePlayer(this.response.player);

          sessionStorage.setItem('token', JSON.stringify(this.response.jwt));
          this.router.navigate(['../../lobby']);
        },
        (error) => {
          this.errorMsg = error.error.message;
        }
      );
    } else this.errorMsg = 'please fill in all the fields';
  }
  goToRegister(): void {
    this.router.navigate(['/register']);
  }
}

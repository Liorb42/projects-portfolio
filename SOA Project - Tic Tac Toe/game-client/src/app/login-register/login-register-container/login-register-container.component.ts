import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { PlayerModel } from 'src/app/models/player.model';

@Component({
  selector: 'app-login-register-container',
  templateUrl: './login-register-container.component.html',
  styleUrls: ['./login-register-container.component.css'],
})
export class LogInRegisterContainerComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}
  login() {
    this.router.navigate(['/login']);
  }
  register() {
    this.router.navigate(['/register']);
  }
}

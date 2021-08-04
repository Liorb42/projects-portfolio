import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-template',
  templateUrl: './header-template.component.html',
  styleUrls: ['./header-template.component.css'],
})
export class HeaderTemplateComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}
  navToHome() {
    this.router.navigate(['/']);
  }
  navToLogIn() {
    this.router.navigate(['/login']);
  }
  navToRegister() {
    this.router.navigate(['/register']);
  }
  navToLobby() {
    this.router.navigate(['/lobby']);
  }
  navToLogOut() {
    this.router.navigate(['/logout']);
  }
}

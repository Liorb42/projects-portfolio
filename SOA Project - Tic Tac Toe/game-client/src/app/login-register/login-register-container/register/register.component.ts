import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameHttpService } from '../../../services/context.service';
import { RegisterResModel } from '../../../models/register-response.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  name: String;
  email: String;
  password: String;
  // confirmPassword: String;
  // isPasswordMatch: Boolean;

  private _confirmPassword: String;
  set confirmPassword(value: String) {
    this._confirmPassword = value;
    this.isPasswordMatch = this.password == this.confirmPassword;
  }
  get confirmPassword(): String {
    return this._confirmPassword;
  }

  private _isPasswordMatch: Boolean;
  set isPasswordMatch(value: Boolean) {
    this._isPasswordMatch = value;
  }
  get isPasswordMatch(): Boolean {
    return this._isPasswordMatch;
  }

  response: RegisterResModel;
  serverMsg: String;

  constructor(private httpService: GameHttpService, private router: Router) {}

  ngOnInit(): void {}

  register(name: string, email: String, password: string) {
    if (name && email && password) {
      this.httpService.registerPlayer(name, email, password).subscribe(
        (res) => {
          this.response = res;
          this.serverMsg = res.message;
        },
        (error) => {
          this.serverMsg = error.error.message;
        }
      );
    }
  }
  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}

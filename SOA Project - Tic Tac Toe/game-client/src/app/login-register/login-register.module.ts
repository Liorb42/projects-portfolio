import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogInRegisterContainerComponent } from './login-register-container/login-register-container.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './login-register-container/register/register.component';
import { LogInComponent } from './login-register-container/login/login.component';

@NgModule({
  declarations: [
    LogInRegisterContainerComponent,
    LogInComponent,
    RegisterComponent,
  ],
  imports: [CommonModule, FormsModule],
})
export class LoginRegisterModule {}

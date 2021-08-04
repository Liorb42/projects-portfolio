import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LobbyContainerComponent } from './lobby/lobby-container/lobby-container.component';
import { LogInRegisterContainerComponent } from './login-register/login-register-container/login-register-container.component';
import { LogInComponent } from './login-register/login-register-container/login/login.component';
import { RegisterComponent } from './login-register/login-register-container/register/register.component';
import { LogoutComponent } from './logout/logout/logout.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthGuardService } from './services/auth-guard.service';
import { XoGameContainerComponent } from './xo-game/xo-game-container/xo-game-container.component';

const routes: Routes = [
  { path: '', component: LogInRegisterContainerComponent },
  { path: 'login', component: LogInComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'lobby',
    component: LobbyContainerComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'game',
    component: XoGameContainerComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'logout',
    component: LogoutComponent,
    canActivate: [AuthGuardService],
  },
  { path: '404', component: PageNotFoundComponent },
  { path: '**', redirectTo: '/404' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

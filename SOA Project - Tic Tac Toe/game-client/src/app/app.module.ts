import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderTemplateComponent } from './header-template/header-template.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginRegisterModule } from './login-register/login-register.module';
import { HttpClientModule } from '@angular/common/http';
import { LobbyModule } from './lobby/lobby.module';
import { XoGameModule } from './xo-game/xo-game.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoggedInPlayerComponent } from './logged-in-player/logged-in-player.component';

@NgModule({
  declarations: [AppComponent, HeaderTemplateComponent, PageNotFoundComponent, LoggedInPlayerComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    LoginRegisterModule,
    LobbyModule,
    XoGameModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

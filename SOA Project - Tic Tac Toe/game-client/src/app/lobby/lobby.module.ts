import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { LobbyContainerComponent } from './lobby-container/lobby-container.component';
import { ConnectedPlayersComponent } from './lobby-container/connected-players/connected-players.component';
import { LobbyRoutingModule } from './lobby-routing.module';
import { ChatModule } from '../chat/chat.module';
import { FormsModule } from '@angular/forms';
import { GameRequestComponent } from './lobby-container/game-request/game-request.component';
import { BtnRequestGameComponent } from './lobby-container/btn-request-game/btn-request-game.component';

@NgModule({
  declarations: [
    LobbyContainerComponent,
    ConnectedPlayersComponent,
    GameRequestComponent,
    BtnRequestGameComponent,
  ],
  imports: [CommonModule, LobbyRoutingModule, ChatModule],
  exports: [ConnectedPlayersComponent],
})
export class LobbyModule {}

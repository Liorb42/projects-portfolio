import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { XoGameContainerComponent } from './xo-game-container/xo-game-container.component';
import { XoGameComponent } from './xo-game-container/xo-game/xo-game.component';
import { ChatModule } from '../chat/chat.module';

@NgModule({
  declarations: [XoGameContainerComponent, XoGameComponent],
  imports: [CommonModule, ChatModule],
  exports: [XoGameContainerComponent],
})
export class XoGameModule {}

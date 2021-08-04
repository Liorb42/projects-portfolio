import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LobbyContainerComponent } from './lobby-container/lobby-container.component';

const routes: Routes = [{ path: '', component: LobbyContainerComponent }];
@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class LobbyRoutingModule {}

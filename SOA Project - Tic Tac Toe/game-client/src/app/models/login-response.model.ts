import { PlayerModel } from './player.model';

export class LogInResModel {
  constructor(public player: PlayerModel, public jwt: String = '') {}
}

export class PlayerModel {
  constructor(
    public id: number = 0,
    public name: string = '',
    public totalWins: number = 0,
    public isPlaying: boolean = false,
    public currentGameId: number = 0
  ) {}
}

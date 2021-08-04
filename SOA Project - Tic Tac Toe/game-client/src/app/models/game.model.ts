export class GameModel {
  constructor(
    public id: number = 0,
    public playerXId: number = 0,
    public playerOId: number = 0,
    public gameMtx: string[][],
    public playerTurn: number = 0,
    public isGameWon: boolean = false,
    public winnerId: number = 0,
    public isMoveValid: boolean = false
  ) {}
}

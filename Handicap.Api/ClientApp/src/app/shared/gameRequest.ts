export class GameRequest {
    id: string;
    playerOnePoints: number;
    playerTwoPoints: number;

    constructor(id: string, playerOnePoints: number, playerTwoPoints: number) {
        this.id = id;
        this.playerOnePoints = playerOnePoints;
        this.playerTwoPoints = playerTwoPoints;
    }
}

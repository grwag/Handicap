import { Player } from './player';

export interface Game {
    id: string;
    tenantId: string;
    date: Date;
    isFinished: boolean;
    matchDayId: string;
    playerOne: Player;
    playerOnePoints: number;
    playerOneRequiredPoints: number;
    playerTwo: Player;
    playerTwoPoints: number;
    playerTwoRequiredPoints: number;
    gameType: number;
}

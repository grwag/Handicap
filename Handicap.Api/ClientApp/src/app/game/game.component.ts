import { Component, OnInit, Input } from '@angular/core';
import { Game } from '../shared/game';
import { GameService } from '../shared/services/game.service';
import { GameRequest } from '../shared/gameRequest';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  @Input() game: Game;
  gameRequest = new GameRequest(this.game.id, this.game.playerOnePoints, this.game.playerTwoPoints);

  constructor(private gameService: GameService) {
  }

  ngOnInit() {
  }

  updateGame(){
    this.gameService.updateGame(this.gameRequest)
    .subscribe(game => {
      this.game = game;
    });
  }
}

import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Player } from '../../shared/player';
import { PlayerService } from '../../shared/services/player.service';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../shared/services/game.service';
import { Game } from '../../shared/game';

@Component({
  selector: 'app-player-games',
  templateUrl: './player-games.component.html',
  providers: [PlayerService, GameService],
  styleUrls: ['./player-games.component.css']
})
export class PlayerGamesComponent implements OnInit {

  player: Player;

  constructor(
    private playerService: PlayerService,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.playerService.getPlayer(this.route.snapshot.params.id)
      .subscribe(player => {
        this.player = player;
      },
      error => {
        console.log(error);
      });
  }

  isWinner(game: Game): boolean {
    if (this.player.id === game.playerOne.id) {
      return game.playerOnePoints >= game.playerOneRequiredPoints;
    }
    return game.playerTwoPoints >= game.playerTwoRequiredPoints;
  }
}

import { Component, OnInit, Input, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { PlayerService } from '../shared/services/player.service';
import { Player } from '../shared/player';
import { Router } from '@angular/router';
import { PlayerRequest } from '../shared/playerRequest';

@Component({
  selector: 'app-player-detail',
  templateUrl: './player-detail.component.html',
  styleUrls: ['./player-detail.component.css']
})
export class PlayerDetailComponent implements OnInit {
  @Input() player: Player;
  playerRequest: PlayerRequest;
  gamesPlayed: number;
  detailsAreLoading: boolean;

  constructor(private playerService: PlayerService, private router: Router) {
    this.detailsAreLoading = true;
  }

  ngOnInit() {
  }

  updatePlayer(): void {
    this.playerService.updatePlayer(this.playerRequest, this.player.id)
    .subscribe(player => {
      console.log('updated player: ' + this.player.id);
      this.router.navigate(['/players']);
      this.router.navigate(['/players/' + this.player.id]);
    });
  }

  deletePlayer(): void {
    this.playerService.deletePlayer(this.player.id)
      .subscribe(() => {
        console.log('Deleted player: ' + this.player.id);
        this.router.navigate(['/players']);
      });
  }

  setPlayerRequest(){
    console.log('set request');
    this.playerRequest = new PlayerRequest(this.player.firstName, this.player.lastName, this.player.handicap);
  }

  playerWasEdited() {
    if (this.playerRequest.firstName !== this.player.firstName) {
      return true;
    }

    if (this.playerRequest.lastName !== this.player.lastName) {
      return true;
    }

    return this.playerRequest.handicap !== this.player.handicap;
  }
}

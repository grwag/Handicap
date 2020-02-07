import { Component, OnInit, Input, OnChanges, SimpleChange, SimpleChanges, Inject } from '@angular/core';
import { PlayerService } from '../../shared/services/player.service';
import { PlayerStats } from '../../shared/playerStats';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-player-detail',
  templateUrl: './player-detail.component.html',
  providers: [PlayerService],
  styleUrls: ['./player-detail.component.css']
})
export class PlayerDetailComponent implements OnInit {
  playerStats: PlayerStats;
  statsAreLoading: boolean;

  constructor(
    private playerDetailsSheetRef: MatBottomSheetRef<PlayerDetailComponent>,
    private playerService: PlayerService,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data,
    private route: ActivatedRoute
  ) {
    this.statsAreLoading = true;
  }

  ngOnInit() {
    this.loadPlayerStats();
  }

  updatePlayer() {
    const returnUrl = '/players';
    this.playerService.updatePlayer(this.data.playerRequest, this.data.playerId)
      .subscribe(() => {
        this.playerDetailsSheetRef.dismiss({ url: returnUrl });
      });
  }

  goToGames() {
    const returnUrl = '/players/' + this.data.playerId + '/games';
    this.playerDetailsSheetRef.dismiss({ url: returnUrl });
  }

  deletePlayer() {
    this.playerService.deletePlayer(this.data.playerId).subscribe(res => {
      this.playerDetailsSheetRef.dismiss({ url: '/players' });
    },
      error => {
        this.playerDetailsSheetRef.dismiss({ error: error.error.Error.ErrorMessage, url: '/players' });
      });
  }

  loadPlayerStats() {
    this.playerService.getPlayerGames(this.data.playerId)
      .subscribe(
        response => {
          if (!response.error) {
            this.playerStats = new PlayerStats(response.totalCount);
            this.statsAreLoading = false;
          }
        });
  }

}

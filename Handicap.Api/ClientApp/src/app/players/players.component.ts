import { Component, OnInit, OnChanges, SimpleChanges, AfterViewInit, Input, Inject } from '@angular/core';
import { PlayerService } from '../shared/services/player.service';
import { Player } from '../shared/player';
import { PlayerRequest } from '../shared/playerRequest';
import { MatPaginator, MAT_BOTTOM_SHEET_DATA, MatBottomSheet, MatBottomSheetRef } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { PlayersDataSource } from '../shared/dataSources/playersDataSource';
import { FormControl, FormGroupDirective, NgForm, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { ViewChild } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';

import { ActivatedRoute, Router } from '@angular/router';
import { merge, Observable, of as observableOf } from 'rxjs';
import { PlayerStats } from '../shared/playerStats';
import { tap } from 'rxjs/operators';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'player-details-sheet',
  templateUrl: 'player-details-sheet.html',
  providers: [PlayerService],
  styleUrls: ['./players.component.css']
})
// tslint:disable-next-line:component-class-suffix
export class PlayerDetailsSheet implements OnInit {
  playerStats: PlayerStats;
  statsAreLoading: boolean;

  constructor(
    private playerDetailsSheetRef: MatBottomSheetRef<PlayerDetailsSheet>,
    private playerService: PlayerService,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data,
  ) {
    this.statsAreLoading = true;
    console.log(data);
  }

  ngOnInit() {
    this.loadPlayerStats();
  }

  updatePlayer() {
    console.log(this.data.playerRequest);
    const returnUrl = '/players';
    this.playerService.updatePlayer(this.data.playerRequest, this.data.playerId)
      .subscribe(() => {
        this.playerDetailsSheetRef.dismiss(returnUrl);
      });
  }

  goToGames() {
    console.log('gotogames');
    const returnUrl = '/players/' + this.data.playerId + '/games';
    this.playerDetailsSheetRef.dismiss(returnUrl);
  }

  deletePlayer() {
    this.playerService.deletePlayer(this.data.playerId)
      .toPromise()
      .then(() => {
        this.playerDetailsSheetRef.dismiss('/players');
      });
  }

  loadPlayerStats() {
    console.log('load games');
    this.playerService.getPlayerGames(this.data.playerId)
      .subscribe(
        response => {
          if (!response.error) {
            this.playerStats = new PlayerStats(response.totalCount);
            console.log(this.playerStats);
            this.statsAreLoading = false;
          }
        });
  }


}
@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  providers: [PlayerService],
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit, AfterViewInit {
  dataSource: PlayersDataSource;
  selectedPlayer: Player;
  selectedPlayerStats: PlayerStats;
  totalPlayers: number;
  statsAreLoading: boolean;
  playerRequest: PlayerRequest;
  selectedPlayerId: string;
  displayedColumns: string[] = ['firstName', 'lastName', 'handicap'];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  form = new FormGroup({
    firstName: new FormControl('', [
      Validators.required
    ]),
    lastName: new FormControl('', [
      Validators.required
    ]),
    handicap: new FormControl('', [
      Validators.required,
      this.validateHandicap
    ])
  });

  constructor(
    private playerService: PlayerService,
    private router: Router,
    private route: ActivatedRoute,
    private playerDetailsSheet: MatBottomSheet) {
  }

  ngOnInit() {
    this.setTotalPlayers();
    this.dataSource = new PlayersDataSource(this.playerService);
    this.dataSource.loadPlayers('FirstName', false, 10, 0);

    const id = this.route.snapshot.params.id;
    if (id != null) {
      this.playerService.getPlayer(id)
        .subscribe(player => {
          this.router.navigate(['/players/' + player.id]);
          this.onSelect(player);
        });
    }
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadPlayersPage())
      )
      .subscribe();
  }

  onSelect(player: Player): void {
    this.router.navigate(['/players/' + player.id]);
    const request = this.getPlayerRequest(player);
    const ref = this.playerDetailsSheet.open(PlayerDetailsSheet,
      {
        data: {
          player,
          playerId: player.id,
          playerRequest: request
        }
      });

    ref.afterDismissed().subscribe(returnUrl => {
      console.log('after dismissed');
      console.log(returnUrl);
      if (returnUrl) {
        this.router.navigate([returnUrl]);
      } else {
        this.router.navigate(['/players']);
      }
    });
  }

  getPlayerRequest(player: Player): PlayerRequest {
    const request = new PlayerRequest(player.firstName, player.lastName, player.handicap);
    return request;
  }

  loadPlayersPage() {
    this.dataSource.loadPlayers(
      this.sort.active,
      (this.sort.direction === 'desc'),
      this.paginator.pageSize,
      this.paginator.pageIndex
    );
  }

  setTotalPlayers() {
    this.playerService.getNumberOfTotalPlayers()
      .subscribe(res => {
        this.totalPlayers = res.totalCount;
      });
  }

  getPlayerDetails(id: string): void {
    console.log('get details');
    this.playerService.getPlayer(id)
      .subscribe(player => {
        this.selectedPlayer = player;
        // this.setPlayerRequest();
      });
  }

  createPlayer() {
    const playerRequest = new PlayerRequest(
      this.form.controls.firstName.value,
      this.form.controls.lastName.value,
      this.form.controls.handicap.value
    );
    this.playerService.createPlayer(playerRequest)
      .subscribe(player => {
        this.onSelect(player);
        this.dataSource.loadPlayers('FirstName', false, this.paginator.pageSize, this.paginator.pageIndex);
        this.setTotalPlayers();
      });
  }

  validateHandicap(formControl: FormControl) {
    const value = formControl.value;

    if (value % 5 === 0 && (value >= 0 && value <= 100)) {
      return null;
    }

    return {
      handicap: {
        parsedHandicap: value
      }
    };
  }

}

import { Component, OnInit, OnChanges, SimpleChanges, AfterViewInit } from '@angular/core';
import { PlayerService } from '../shared/services/player.service';
import { Player } from '../shared/player';
import { PlayerRequest } from '../shared/playerRequest';
import { MatPaginator } from '@angular/material';
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

  constructor(private playerService: PlayerService, private router: Router, private route: ActivatedRoute) {
    this.statsAreLoading = true;
  }

  ngOnInit() {
    this.setTotalPlayers();
    this.dataSource = new PlayersDataSource(this.playerService);
    this.dataSource.loadPlayers('FirstName', false, 10, 0);

    const id = this.getCurrentPlayerId();
    if (id != null) {
      this.playerService.getPlayer(id)
        .subscribe(player => {
          this.selectedPlayer = player;
          this.setPlayerRequest();
          this.loadGames();
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
    this.selectedPlayer = player;
    this.setPlayerRequest();
    this.loadGames();
    console.log('selected');
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
        this.setPlayerRequest();
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

  updatePlayer(): void {
    this.playerService.updatePlayer(this.playerRequest, this.selectedPlayer.id)
      .subscribe(player => {
        console.log('updated player: ' + this.selectedPlayer.id);
        this.dataSource.loadPlayers('FirstName', false, this.paginator.pageSize, this.paginator.pageIndex);
        this.router.navigate(['/players/' + this.selectedPlayer.id]);
      });
  }

  deletePlayer(): void {
    const id = this.getCurrentPlayerId();

    this.selectedPlayer = null;

    this.playerService.deletePlayer(id)
      .subscribe(() => {
        console.log('Deleted player: ' + id);
        this.router.navigate(['/players']);
      });
  }

  loadGames() {
    console.log('load games');
    this.playerService.getPlayerGames(this.selectedPlayer.id)
      .subscribe(
        response => {
          if (!response.error) {
            this.selectedPlayerStats = new PlayerStats(response.totalCount);
            this.statsAreLoading = false;
          }
        });
  }

  setPlayerRequest() {
    console.log('set request');
    this.playerRequest = new PlayerRequest(this.selectedPlayer.firstName, this.selectedPlayer.lastName, this.selectedPlayer.handicap);
  }

  getCurrentPlayerId(): string {
    const id = this.route.snapshot.params.id;
    console.log(id);
    return id;
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

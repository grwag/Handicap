import { Component, OnInit, OnChanges, SimpleChanges, AfterViewInit, Input, Inject } from '@angular/core';
import { PlayerService } from '../../shared/services/player.service';
import { Player } from '../../shared/player';
import { PlayerRequest } from '../../shared/playerRequest';
import { MatPaginator, MatBottomSheet, MatSnackBar } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { PlayersDataSource } from '../../shared/dataSources/playersDataSource';
import { ViewChild } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { merge, of as observableOf } from 'rxjs';
import { PlayerStats } from '../../shared/playerStats';
import { tap } from 'rxjs/operators';
import { PlayerDetailComponent } from '../player-detail/player-detail.component';
import { Validators, FormControl, FormGroup } from '@angular/forms';

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
    private playerDetailsSheet: MatBottomSheet,
    private snackBar: MatSnackBar) {
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
        },
        error => {
          console.log(error);
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
    const ref = this.playerDetailsSheet.open(PlayerDetailComponent,
      {
        data: {
          player,
          playerId: player.id,
          playerRequest: request
        }
      });

    ref.afterDismissed().subscribe(data => {
      if (data) {
        if (data.error) {
          this.snackBar.open(data.error,
            'Error',
            { duration: 5000 });
          this.router.navigate(['/players']);
        } else if (data.url) {
          this.router.navigate([data.url]);
        }
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
      },
      error => {
        console.log(error);
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
        this.setTotalPlayers();
        this.dataSource.loadPlayers();
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

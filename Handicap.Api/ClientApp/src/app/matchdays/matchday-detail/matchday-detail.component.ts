import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';

import { MatchdayService } from '../../shared/services/matchday.service';
import { PlayerService } from '../../shared/services/player.service';
import { GameService } from '../../shared/services/game.service';

import { Matchday } from '../../shared/matchday';
import { Game } from '../../shared/game';
import { Player } from '../../shared/player';
import { GameRequest } from '../../shared/gameRequest';
import { GamesDataSource } from '../../shared/dataSources/gamesDataSource';

import { MatSnackBar, MatBottomSheet, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material';
import { FormGroup } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'add-remove-players-dialog',
  templateUrl: 'add-remove-players-dialog.html',
  providers: [MatchdayService],
})
// tslint:disable-next-line:component-class-suffix
export class AddRemovePlayersDialog {

  playersToAdd: string[] = [];
  playersToRemove: string[] = [];
  constructor(
    private addRemovePlayersDialogRef: MatBottomSheetRef<AddRemovePlayersDialog>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data,
    private matchdayService: MatchdayService) {
  }

  checkboxChanged(event) {
    const playerId = event.source.value;
    if (this.data.matchdayPlayers.some(p => p.id === playerId)) {
      this.handleMatchdayPlayer(event, playerId);
    } else {
      this.handlePlayer(event, playerId);
    }

  }

  handlePlayer(event, playerId) {
    if (event.checked) {
      this.playersToAdd.push(playerId);
    } else {
      if (this.playersToAdd.includes(playerId)) {
        const index = this.playersToAdd.indexOf(playerId);
        this.playersToAdd.splice(index, 1);
      }
    }
  }

  handleMatchdayPlayer(event, playerId) {
    if (event.checked) {
      if (!this.data.matchdayPlayers.some(p => p.id === playerId)) {
        this.playersToAdd.push(playerId);
      }

      if (this.playersToRemove.some(p => p === playerId)) {
        this.removeFromArray(playerId, this.playersToRemove);
      }
    } else {
      if (this.playersToRemove.some(p => p === playerId)) {
        this.removeFromArray(playerId, this.playersToRemove);
      } else {
        this.playersToRemove.push(playerId);
      }
    }
  }

  removeFromArray(playerId: string, arr: string[]) {
    const index = this.playersToRemove.indexOf(playerId);
    arr.splice(index, 1);
  }

  addRemove() {
    this.matchdayService.addRemovePlayers(this.data.matchdayId, this.playersToAdd, this.playersToRemove)
      .then(() => {
        this.addRemovePlayersDialogRef.dismiss();
      });
  }
}

@Component({
  selector: 'app-matchday-detail',
  templateUrl: './matchday-detail.component.html',
  providers: [MatchdayService, PlayerService, GameService],
  styleUrls: ['./matchday-detail.component.css']
})
export class MatchdayDetailComponent implements OnInit {

  matchday: Matchday;
  dataSource: GamesDataSource;

  matchdayId: string = this.route.snapshot.params.id;

  activeGames: Game[] = [];
  finishedGames: Game[] = [];
  matchdayPlayers: Player[] = [];
  availablePlayers: Player[] = [];

  form: FormGroup;

  constructor(
    private matchdayService: MatchdayService,
    private playerService: PlayerService,
    private gameService: GameService,
    private route: ActivatedRoute,
    private router: Router,
    private cdRef: ChangeDetectorRef,
    private snackBar: MatSnackBar,
    private addRemovePlayersDialog: MatBottomSheet,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.loadGames();
    this.setPlayers();
    this.matchdayService.getMatchday(this.route.snapshot.params.id)
      .subscribe(md => {
        this.matchday = md;
      },
        error => {
          console.log(error);
        });
  }

  loadGames() {
    this.matchdayService.getGames(this.matchdayId)
      .subscribe(res => {
        this.activeGames = res.payload.filter(g => !g.isFinished);
        this.finishedGames = res.payload.filter(g => g.isFinished);

        this.cdRef.detectChanges();
      },
        error => {
          console.log(error);
        });
  }

  addRemovePlayers() {
    const ref = this.addRemovePlayersDialog.open(AddRemovePlayersDialog,
      {
        data: {
          matchdayPlayers: this.matchdayPlayers,
          availablePlayers: this.availablePlayers,
          matchdayId: this.matchdayId
        }
      });
    ref.afterDismissed().subscribe(() => {
      this.setPlayers();
    },
      error => {
        console.log(error);
        this.setPlayers();
      });
  }

  setPlayers() {
    this.matchdayService.getPlayers(this.matchdayId)
      .then(res => {
        this.matchdayPlayers = res.payload;

        this.setAvailablePlayers();
      });
  }

  async setAvailablePlayers() {
    this.matchdayService.getAvailablePlayers(this.matchdayId)
      .subscribe(res => {
        this.availablePlayers = res.payload;
      },
        error => {
          console.log(error);
        });
  }

  nextGame() {
    this.matchdayService.newGame(this.matchdayId)
      .subscribe(
        () => {
          this.loadGames();
        },
        error => this.snackBar.open(error.error.Error.ErrorMessage,
          'Error',
          { duration: 5000 })
      );
  }

  saveGame(gameId: string, playerOnePoints: number, playerTwoPoints: number, game: Game) {
    if (playerOnePoints < game.playerOneRequiredPoints && playerTwoPoints < game.playerTwoRequiredPoints) {
      this.snackBar.open('Someone has to win the game ;).', 'Error',
        { duration: 5000 });

      return;
    }

    const gameRequest = new GameRequest(gameId, playerOnePoints, playerTwoPoints);
    this.gameService.updateGame(gameRequest)
      .subscribe(
        () => {
          this.loadGames();
        },
        error => this.snackBar.open(error.error.Error.ErrorMessage,
          'Error',
          { duration: 5000 })
      );
  }

  finishMatchday() {
    this.matchdayService.finalize(this.matchdayId)
      .subscribe((md) => {
        console.log(md);
        this.router.navigate(['/matchdays']);
      },
        error => {
          this.snackBar.open(error.error.Error.ErrorMessage,
            'Error',
            { duration: 5000 });
        }
      );
  }

  isWinner(game: Game, player: Player): boolean {
    if (player === null) {
      return false;
    }

    if (player.id === game.playerOne.id) {
      return game.playerOnePoints >= game.playerOneRequiredPoints;
    }
    return game.playerTwoPoints >= game.playerTwoRequiredPoints;
  }

}

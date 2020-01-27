import { Game } from '../game';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable, of, animationFrameScheduler } from 'rxjs';
import { Player } from '../player';
import { GameService } from '../services/game.service';
import { catchError, finalize } from 'rxjs/operators';

export class GamesDataSource implements DataSource<Game> {
    private gamesSubject = new BehaviorSubject<Game[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private gameService: GameService) { }

    connect(cllectionViewer: CollectionViewer): Observable<Game[]> {
        return this.gamesSubject.asObservable();
    }

    disconnect( collectionViewer: CollectionViewer): void {
        this.gamesSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlayerGames(playerId: string, orderBy: string = 'Date', desc: boolean = false, pageSize: number = 10, page: number = 0) {
        this.loadingSubject.next(true);

        this.gameService.getPlayerGames(playerId, orderBy, desc, pageSize, page).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        ).subscribe(games => {
            const finishedGames = games.filter(g => g.isFinished);
            this.gamesSubject.next(finishedGames);
        });
    }

    loadMatchdayGames(matchdayId: string, orderBy: string = 'Date', desc: boolean = false, pageSize: number = 10, page: number = 0) {
        this.loadingSubject.next(true);

        this.gameService.getMatchDayGames(matchdayId, orderBy, desc, pageSize, page).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        ).subscribe(games => {
            const finishedGames = games.filter(g => g.isFinished);
            this.gamesSubject.next(finishedGames);
        });
    }
}

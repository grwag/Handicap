import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Player } from '../player';
import { PlayerService } from '../services/player.service';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';

export class PlayersDataSource implements DataSource<Player> {
    private playersSubject = new BehaviorSubject<Player[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private playerService: PlayerService) { }

    connect(collectionViewer: CollectionViewer): Observable<Player[]> {
        return this.playersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.playersSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlayers(orderBy: string = 'LastName', desc: boolean = false, pageSize: number = 10, page: number = 0) {
        this.loadingSubject.next(true);

        this.playerService.getPlayers(orderBy, desc, pageSize, page).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        ).subscribe(players => this.playersSubject.next(players));
    }
}

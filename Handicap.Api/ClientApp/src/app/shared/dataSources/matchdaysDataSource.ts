import { DataSource } from '@angular/cdk/table';
import { Matchday } from '../matchday';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { MatchdayService } from '../services/matchday.service';
import { CollectionViewer } from '@angular/cdk/collections';
import { catchError, finalize } from 'rxjs/operators';

export class MatchdaysDataSource implements DataSource<Matchday> {
    private matchdaysSubject = new BehaviorSubject<Matchday[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private matchdayService: MatchdayService) { }

    connect(collectionVieweer: CollectionViewer): Observable<Matchday[]> {
        return this.matchdaysSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.matchdaysSubject.complete();
        this.loadingSubject.complete();
    }

    loadMatchdays(
        orderby: string = 'date',
        desc: boolean = false,
        pageSize: number = 10,
        page: number = 0
    ) {
        this.loadingSubject.next(true);

        this.matchdayService.getMatchdays(orderby, desc, pageSize, page)
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            ).subscribe(matchdays => this.matchdaysSubject.next(matchdays));
    }
}

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { Observable, pipe } from 'rxjs';
import { HandicapResponse } from '../handicapResponse';
import { Game } from '../game';
import { GameRequest } from '../gameRequest';

@Injectable()
export class GameService {

  baseApiUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_API_URL') baseApiUrl: string) {
    this.baseApiUrl = baseApiUrl;
  }

  getPlayerGames(
    playerId: string,
    orderBy: string = 'LastName',
    desc: boolean = false,
    pageSize: number = 10,
    page: number = 0): Observable<Game[]> {

    const uri = this.baseApiUrl + 'players/' + playerId + '/games';

    return this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', orderBy)
        .set('desc', desc.toString())
        .set('pageSize', pageSize.toString())
        .set('page', page.toString())
    }).pipe(
      map(res => res.payload)
    );
  }

  getNumberOfPlayerGames(playerId: string): Observable<HandicapResponse> {
    const count = new Number(0);
    const uri = this.baseApiUrl + 'players/' + playerId + '/games';
    return this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', 'date')
        .set('desc', false.toString())
        .set('pageSize', count.toString())
        .set('page', count.toString())
    }).pipe();
  }

  getNumberOfMatchdayGames(matchdayId: string): Observable<HandicapResponse> {
    const count = new Number(0);
    const uri = this.baseApiUrl + 'matchdays/' + matchdayId + '/games';
    return this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', 'date')
        .set('desc', false.toString())
        .set('pageSize', count.toString())
        .set('page', count.toString())
    }).pipe();
  }

  getMatchDayGames(
    matchDayId: string,
    orderBy: string = 'LastName',
    desc: boolean = false,
    pageSize: number = 10,
    page: number = 0): Observable<Game[]> {

    const uri = this.baseApiUrl + 'matchDays/' + matchDayId + '/games';

    return this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', orderBy)
        .set('desc', desc.toString())
        .set('pageSize', pageSize.toString())
        .set('page', page.toString())
    }).pipe(
      map(res => res.payload)
    );
  }

  updateGame(gameRequest: GameRequest): Observable<Game>{
    const gameUri = this.baseApiUrl + 'games/' + gameRequest.id;
    return this.http.put<Game>(gameUri, gameRequest);
  }
}

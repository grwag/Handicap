import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { Observable, pipe } from 'rxjs';
import { HandicapResponse } from '../handicapResponse';
import { Player } from '../player';
import { PlayerRequest } from '../playerRequest';

@Injectable()
export class PlayerService {

  baseApiUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_API_URL') baseApiUrl: string) {
    this.baseApiUrl = baseApiUrl + 'players/';
  }

  getPlayers(
    orderBy: string = 'LastName',
    desc: boolean = false,
    pageSize: number = 10,
    page: number = 0): Observable<Player[]> {

    return this.http.get<HandicapResponse>(this.baseApiUrl, {
      params: new HttpParams()
        .set('orderBy', orderBy)
        .set('desc', desc.toString())
        .set('pageSize', pageSize.toString())
        .set('page', page.toString())
    }).pipe(
      map(res => res.payload)
    );
  }

  getNumberOfTotalPlayers(): Observable<HandicapResponse> {
    const count = new Number(0);
    return this.http.get<HandicapResponse>(this.baseApiUrl, {
      params: new HttpParams()
        .set('orderBy', 'LastName')
        .set('desc', false.toString())
        .set('pageSize', count.toString())
        .set('page', count.toString())
    }).pipe();
  }

  getPlayer(id: string): Observable<Player> {
    const playerUrl = this.baseApiUrl + id;
    return this.http.get<Player>(playerUrl).pipe();
  }

  updatePlayer(playerRequest: PlayerRequest, id: string): Observable<Player> {
    const playerUrl = this.baseApiUrl + id;
    return this.http.put<Player>(playerUrl, playerRequest);
  }

  createPlayer(playerRequest: PlayerRequest): Observable<Player> {
    console.log(playerRequest);
    return this.http.post<Player>(this.baseApiUrl, playerRequest);
  }

  deletePlayer(id: string): Observable<any> {
    const playerUrl = this.baseApiUrl + id;
    return this.http.delete(playerUrl).pipe();
  }

  getPlayerGames(id: string): Observable<HandicapResponse> {
    const gamesUrl = this.baseApiUrl + id + '/games';
    return this.http.get<HandicapResponse>(gamesUrl).pipe();
  }
}

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HandicapResponse } from '../handicapResponse';
import { Matchday } from '../matchday';
import { map, mergeMap } from 'rxjs/operators';
import { Game } from '../game';
import { Player } from '../player';

@Injectable()
export class MatchdayService {

  baseApiUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_API_URL') baseApiUrl: string) {
    this.baseApiUrl = baseApiUrl + 'matchdays';
  }

  getMatchdays(
    orderBy: string = 'date',
    desc: boolean = false,
    pageSize: number = 10,
    page: number = 0): Observable<Matchday[]> {

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

  getTotalMatchdays(
    orderBy: string = 'date',
    desc: boolean = false,
    pageSize: number = 10,
    page: number = 0): Observable<HandicapResponse> {

    return this.http.get<HandicapResponse>(this.baseApiUrl, {
      params: new HttpParams()
        .set('orderBy', orderBy)
        .set('desc', desc.toString())
        .set('pageSize', pageSize.toString())
        .set('page', page.toString())
    }).pipe();
  }

  getGames(id: string): Observable<HandicapResponse> {
    const uri = this.baseApiUrl + '/' + id + '/games';
    return this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', 'date')
        .set('desc', true.toString())
        .set('pageSize', '99999')
        .set('page', '0')
    }).pipe();
  }

  async getPlayers(id: string) {
    const uri = this.baseApiUrl + '/' + id + '/players';
    return await this.http.get<HandicapResponse>(uri, {
      params: new HttpParams()
        .set('orderBy', 'firstName')
        .set('desc', false.toString())
        .set('pageSize', '99999')
        .set('page', '0')
    }).toPromise();
  }

  getMatchday(id: string): Observable<Matchday> {
    return this.http.get<Matchday>(this.baseApiUrl + '/' + id).pipe();
  }

  getOpenMatchdays(): Observable<HandicapResponse> {
    const uri = this.baseApiUrl + '/open';

    return this.http.get<HandicapResponse>(uri).pipe();
  }

  createMatchday(): Observable<Matchday> {
    return this.http.post<Matchday>(this.baseApiUrl, null)
      .pipe();
  }

  newGame(id: string): Observable<Game> {
    const uri = this.baseApiUrl + '/' + id + '/newgame';
    return this.http.post<Game>(uri, null)
      .pipe();
  }

  finalize(id: string): Observable<Game> {
    const uri = this.baseApiUrl + '/' + id + '/finalize';
    return this.http.post<Game>(uri, null)
      .pipe();
  }

  addPlayers(id: string, playersToAdd: string[]) {
    const uri = this.baseApiUrl + '/' + id + '/players';
    return this.http.post<Matchday>(uri, { playerIds: playersToAdd })
      .pipe();
  }

  removePlayers(id: string, playersToRemove: string[]) {
    console.log('remove players');
    const uri = this.baseApiUrl + '/' + id + '/players';
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: {
        playerIds: playersToRemove
      }
    };

    return this.http.delete<Matchday>(uri, options)
      .pipe();
  }

  async addRemovePlayers(id: string, playersToAdd: string[], playersToRemove: string[]) {
    const uri = this.baseApiUrl + '/' + id + '/players';
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: {
        playerIds: playersToRemove
      }
    };

    await this.http.post<Matchday>(uri, { playerIds: playersToAdd }).toPromise();
    const matchday = await this.http.delete<Matchday>(uri, options).toPromise();

    return matchday;
  }

  getAvailablePlayers(id: string) {
    const uri = this.baseApiUrl + '/' + id + '/availableplayers';
    return this.http.get<HandicapResponse>(uri)
    .pipe();
  }

}

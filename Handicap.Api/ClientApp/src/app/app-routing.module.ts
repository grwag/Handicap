import { NgModule } from '@angular/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { Routes, RouterModule } from '@angular/router';

import { ConfigComponent } from './config/config.component';
import { PlayersComponent } from './players/players.component';
import { MatchdaysComponent } from './matchdays/matchdays.component';
import { AuthGuard } from './shared/auth/auth.guard.service';
import { PlayerGamesComponent } from './player-games/player-games.component';
import { MatchdayDetailComponent } from './matchday-detail/matchday-detail.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: 'config', component: ConfigComponent, canActivate: [AuthGuard] },
  { path: 'players', component: PlayersComponent, canActivate: [AuthGuard]},
  { path: 'players/:id', component: PlayersComponent, canActivate: [AuthGuard]},
  { path: 'players/:id/games', component: PlayerGamesComponent, canActivate: [AuthGuard]},
  { path: 'matchdays', component: MatchdaysComponent, canActivate: [AuthGuard]},
  { path: 'matchdays/:id', component: MatchdayDetailComponent, canActivate: [AuthGuard]},
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    OAuthModule.forRoot()
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

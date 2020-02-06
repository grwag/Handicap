import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../shared/auth/auth.guard.service';

import { PlayersComponent } from './players/players.component';
import { PlayerGamesComponent } from './player-games/player-games.component';

const routes: Routes = [
    { path: '', component: PlayersComponent, canActivate: [AuthGuard] },
    { path: ':id', component: PlayersComponent, canActivate: [AuthGuard] },
    { path: ':id/games', component: PlayerGamesComponent, canActivate: [AuthGuard] },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PlayersRoutingModule { }


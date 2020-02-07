import { NgModule } from '@angular/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  {
    path: 'config',
    loadChildren: () => import('./config/config.module').then(m => m.ConfigModule),
  },
  {
    path: 'players',
    loadChildren: () => import('./players/players.module').then(m => m.PlayersModule),
  },
  {
    path: 'matchdays',
    loadChildren: () => import('./matchdays/matchdays.module').then(m => m.MatchdaysModule),
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
    OAuthModule.forRoot(),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

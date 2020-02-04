import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';

import { MatCardModule } from '@angular/material';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigComponent } from './config/config.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from 'src/environments/environment';

import { AvatarModule } from 'ngx-avatar';

import { OAuthModule } from 'angular-oauth2-oidc';
import { PlayersComponent, PlayerDetailsSheet } from './players/players.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { MatchdaysComponent } from './matchdays/matchdays.component';
import { GameComponent } from './game/game.component';
import { PlayerGamesComponent } from './player-games/player-games.component';
import { MatchdayDetailComponent, AddRemovePlayersDialog } from './matchday-detail/matchday-detail.component';
import { GameTableComponent } from './game-table/game-table.component';
import { HomeComponent } from './home/home.component';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

@NgModule({
  declarations: [
    AppComponent,
    ConfigComponent,
    NavMenuComponent,
    PlayersComponent,
    PlayerDetailsSheet,
    PlayerDetailComponent,
    MatchdaysComponent,
    GameComponent,
    PlayerGamesComponent,
    MatchdayDetailComponent,
    GameTableComponent,
    HomeComponent,
    AddRemovePlayersDialog,
  ],
  entryComponents: [
    AddRemovePlayersDialog,
    PlayerDetailsSheet,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    MatSlideToggleModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatInputModule,
    MatIconModule,
    MatPaginatorModule,
    MatSortModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatBottomSheetModule,
    MatDividerModule,
    MatListModule,
    MatSelectModule,
    ReactiveFormsModule,
    AvatarModule,
    FormsModule,
    NgbModule,
    OAuthModule.forRoot({
      resourceServer: {
        // allowedUrls: ['https://localhost:5001/api'],
        sendAccessToken: true
      }
    }),
    RouterModule.forRoot([

    ]),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: translateLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(public translate: TranslateService) {
    translate.addLangs(['de', 'en']);
    translate.use('de');
  }
}

export function translateLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

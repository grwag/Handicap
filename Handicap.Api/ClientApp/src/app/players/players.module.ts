import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlayersRoutingModule } from './players-routing.module';
import { PlayersComponent } from './players/players.component';
import { PlayerGamesComponent } from './player-games/player-games.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { CreatePlayerComponent } from './create-player/create-player.component';

// import { GameTableComponent } from '../game-table/game-table.component';

import { TranslateModule } from '@ngx-translate/core';
import { MatCardModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { AvatarModule } from 'ngx-avatar';

import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        PlayersComponent,
        PlayerDetailComponent,
        PlayerGamesComponent,
        CreatePlayerComponent,
    ],
    entryComponents: [
        PlayerDetailComponent
    ],
    imports: [
        CommonModule,
        PlayersRoutingModule,
        MatCardModule,
        TranslateModule,
        FormsModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatPaginatorModule,
        MatSortModule,
        MatSnackBarModule,
        MatTooltipModule,
        AvatarModule,
        SharedModule,
    ]
})
export class PlayersModule { }

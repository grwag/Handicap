
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchdaysComponent } from './matchdays/matchdays.component';
import { MatchdayDetailComponent, AddRemovePlayersDialog } from './matchday-detail/matchday-detail.component';
import { AvatarModule } from 'ngx-avatar';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatTooltipModule,
    MatToolbarModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatListModule,
} from '@angular/material';

import { SharedModule } from '../shared/shared.module';
import { MatchdaysRoutingModule } from './matchdays-routing.module';

@NgModule({
    declarations: [
        MatchdaysComponent,
        MatchdayDetailComponent,
        AddRemovePlayersDialog
    ],
    entryComponents: [
        AddRemovePlayersDialog
    ],
    imports: [
        CommonModule,
        MatchdaysRoutingModule,
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
        MatToolbarModule,
        MatSidenavModule,
        MatCheckboxModule,
        MatListModule,
        AvatarModule,
        SharedModule
    ]
})
export class MatchdaysModule { }

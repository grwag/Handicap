import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameTableComponent } from '../game-table/game-table.component';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';

import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    declarations: [
        GameTableComponent
    ],
    imports: [
        CommonModule,
        MatProgressSpinnerModule,
        MatTableModule,
        MatPaginatorModule,
        TranslateModule,
        MatSortModule,
        MatBottomSheetModule
    ],
    exports: [
        GameTableComponent,
        MatBottomSheetModule
    ]
})
export class SharedModule {

}

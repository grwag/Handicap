import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { AvatarModule } from 'ngx-avatar';

import { MatchdayDetailComponent } from './matchday-detail.component';
import { GameTableComponent } from '../../game-table/game-table.component';
import {
  MatToolbarModule,
  MatCardModule,
  MatFormFieldModule,
  MatSidenavModule,
  MatTooltipModule,
  MatButtonModule,
  MatProgressSpinnerModule,
  MatPaginatorModule,
  MatTableModule,
  MatSnackBarModule,
  MatBottomSheetModule,
  MatBottomSheetRef,
  MAT_BOTTOM_SHEET_DATA
} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { GameService } from 'src/app/shared/services/game.service';
import { RouterTestingModule } from '@angular/router/testing';

describe('MatchdayDetailComponent', () => {
  let component: MatchdayDetailComponent;
  let fixture: ComponentFixture<MatchdayDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        MatchdayDetailComponent,
        GameTableComponent,
      ],
      imports: [
        TranslateModule,
        MatToolbarModule,
        MatCardModule,
        FormsModule,
        MatFormFieldModule,
        AvatarModule,
        MatSidenavModule,
        MatTooltipModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MatPaginatorModule,
        MatTableModule,
        ReactiveFormsModule,
        MatSnackBarModule,
        MatBottomSheetModule,
        RouterTestingModule,
        TranslateModule.forRoot()
      ],
      providers: [
        GameService,
        HttpClient,
        HttpHandler,
        { provide: MatBottomSheetRef, useValue: {}},
        { provide: MAT_BOTTOM_SHEET_DATA, useValue: []},
        { provide: 'BASE_API_URL', useClass: class {}}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchdayDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

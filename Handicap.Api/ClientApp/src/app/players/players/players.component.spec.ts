import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';

import { PlayersComponent } from './players.component';
import { CreatePlayerComponent } from '../create-player/create-player.component';
import { MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatSortModule, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA, MatFormFieldModule, MatBottomSheetModule, MatSnackBarModule, MatInputModule } from '@angular/material';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { PlayerRequest } from 'src/app/shared/playerRequest';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationPlayer } from '@angular/animations';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('PlayersComponent', () => {
  let component: PlayersComponent;
  let fixture: ComponentFixture<PlayersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PlayersComponent,
        CreatePlayerComponent
      ],
      imports: [
        TranslateModule.forRoot(),
        MatProgressSpinnerModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatFormFieldModule,
        FormsModule,
        ReactiveFormsModule,
        MatBottomSheetModule,
        MatSnackBarModule,
        MatInputModule,
        RouterTestingModule,
        NoopAnimationsModule,
      ],
      providers: [
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class {}},
        { provide: MatBottomSheetRef, useValue: {}},
        { provide: MAT_BOTTOM_SHEET_DATA, useValue: {data: {
          player: {
            firstName: 'firstName',
            lastName: 'lastName',
            handicap: 5,
            id: 'player.id'
          },
          playerId: 'player.id',
          playerRequest: new PlayerRequest('firstName', 'lastName', 5)
        }}},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  afterEach(() => { fixture.destroy(); });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

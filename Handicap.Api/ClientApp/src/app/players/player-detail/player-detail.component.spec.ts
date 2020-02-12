import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AvatarModule } from 'ngx-avatar';

import { PlayerDetailComponent } from './player-detail.component';
import { MatCardModule, MatFormFieldModule, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { PlayerRequest } from 'src/app/shared/playerRequest';

describe('PlayerDetailComponent', () => {
  let component: PlayerDetailComponent;
  let fixture: ComponentFixture<PlayerDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PlayerDetailComponent],
      imports: [
        AvatarModule,
        MatCardModule,
        MatFormFieldModule,
        FormsModule,
        MatProgressSpinnerModule,
        RouterTestingModule,
        TranslateModule.forRoot()
      ],
      providers: [
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } },
        { provide: MatBottomSheetRef, useValue: {} },
        {
          provide: MAT_BOTTOM_SHEET_DATA, useValue: {
            data: {
              player: {
                firstName: 'firstName',
                lastName: 'lastName',
                handicap: 5,
                id: 'player.id'
              },
              playerId: 'player.id',
              playerRequest: new PlayerRequest('firstName', 'lastName', 5)
            }
          }
        },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  afterEach(() => { fixture.destroy(); });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

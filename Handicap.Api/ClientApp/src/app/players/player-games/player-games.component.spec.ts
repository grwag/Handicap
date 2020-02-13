import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerGamesComponent } from './player-games.component';
import { GameTableComponent } from 'src/app/game-table/game-table.component';
import { MatProgressSpinnerModule, MatTableModule, MatPaginatorModule } from '@angular/material';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule, HttpClient, HttpHandler } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { GameService } from 'src/app/shared/services/game.service';
import { PlayerService } from 'src/app/shared/services/player.service';

describe('PlayerGamesComponent', () => {
  let component: PlayerGamesComponent;
  let fixture: ComponentFixture<PlayerGamesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PlayerGamesComponent,
        GameTableComponent
      ],
      imports: [
        MatProgressSpinnerModule,
        MatTableModule,
        MatPaginatorModule,
        TranslateModule.forRoot(),
        RouterTestingModule,
        HttpClientModule
      ],
      providers: [
        HttpClient,
        HttpHandler,
        GameService,
        PlayerService,
        { provide: 'BASE_API_URL', useClass: class {}}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerGamesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { GameTableComponent } from './game-table.component';
import { GamesDataSource } from '../shared/dataSources/gamesDataSource';
import { MatPaginatorModule, MatTableModule, MatSortModule } from '@angular/material';
import { TranslateModule } from '@ngx-translate/core';
import { GameService } from '../shared/services/game.service';
import { HttpClient, HttpHandler, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('GameTableComponent', () => {
  let component: GameTableComponent;
  let fixture: ComponentFixture<GameTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        GameTableComponent,
        // GamesDataSource
      ],
      imports: [
        MatProgressSpinnerModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([]),
        BrowserAnimationsModule,
        HttpClientModule
      ],
      providers: [
        GameService,
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } },
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { GameService } from './game.service';
import { HttpClient, HttpHeaders, HttpParams, HttpHandler, HttpClientModule } from '@angular/common/http';

describe('GameService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      GameService,
      HttpClient,
      HttpHandler,
      { provide: 'BASE_API_URL', useClass: class {}}
    ],
    imports: [
      HttpClientModule
    ]
  }));

  it('should be created', () => {
    const service: GameService = TestBed.get(GameService);
    expect(service).toBeTruthy();
  });
});

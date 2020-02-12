import { TestBed } from '@angular/core/testing';

import { PlayerService } from './player.service';
import { HttpClient, HttpHeaders, HttpParams, HttpHandler } from '@angular/common/http';

describe('PlayerService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      PlayerService,
      HttpClient,
      HttpHandler,
      { provide: 'BASE_API_URL', useClass: class {}}
    ]
  }));

  it('should be created', () => {
    const service: PlayerService = TestBed.get(PlayerService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { MatchdayService } from './matchday.service';
import { HttpClient, HttpHeaders, HttpParams, HttpHandler } from '@angular/common/http';

describe('MatchdayService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      MatchdayService,
      HttpClient,
      HttpHandler,
      { provide: 'BASE_API_URL', useClass: class {}}
    ]
  }));

  it('should be created', () => {
    const service: MatchdayService = TestBed.get(MatchdayService);
    expect(service).toBeTruthy();
  });
});

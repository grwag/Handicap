import { TestBed } from '@angular/core/testing';

import { ConfigService } from './config.service';
import { HttpClient, HttpHeaders, HttpParams, HttpHandler } from '@angular/common/http';

describe('ConfigService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      ConfigService,
      HttpClient,
      HttpHandler,
      { provide: 'BASE_API_URL', useClass: class {}}
    ]
  }));

  it('should be created', () => {
    const service: ConfigService = TestBed.get(ConfigService);
    expect(service).toBeTruthy();
  });
});

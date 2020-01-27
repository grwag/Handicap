import { TestBed } from '@angular/core/testing';

import { MatchdayService } from './matchday.service';

describe('MatchdayService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MatchdayService = TestBed.get(MatchdayService);
    expect(service).toBeTruthy();
  });
});

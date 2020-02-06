import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchdayDetailComponent } from './matchday-detail.component';

describe('MatchdayDetailComponent', () => {
  let component: MatchdayDetailComponent;
  let fixture: ComponentFixture<MatchdayDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchdayDetailComponent ]
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

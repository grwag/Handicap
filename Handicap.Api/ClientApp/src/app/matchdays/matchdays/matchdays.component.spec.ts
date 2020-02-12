import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { MatchdaysComponent } from './matchdays.component';
import { MatTableModule, MatPaginatorModule, MatSortModule } from '@angular/material';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('MatchdaysComponent', () => {
  let component: MatchdaysComponent;
  let fixture: ComponentFixture<MatchdaysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        MatchdaysComponent,
      ],
      imports: [
        TranslateModule,
        MatProgressSpinnerModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        RouterTestingModule,
        TranslateModule.forRoot(),
        BrowserAnimationsModule,
        NoopAnimationsModule
      ],
      providers: [
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class {}}
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchdaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

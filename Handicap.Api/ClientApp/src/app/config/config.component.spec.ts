import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TranslateModule } from '@ngx-translate/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule, MatLabel, MatInputModule, MatSelectModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ConfigComponent } from './config.component';
import { CommonModule } from '@angular/common';
import { ConfigRoutingModule } from './config-routing.module';
import { ConfigService } from './config.service';
import { HttpClient, HttpHandler } from '@angular/common/http';

describe('ConfigComponent', () => {
  let component: ConfigComponent;
  let fixture: ComponentFixture<ConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConfigComponent],
      imports: [
        TranslateModule.forRoot(),
        MatSlideToggleModule,
        MatCardModule,
        FormsModule,
        CommonModule,
        ConfigRoutingModule,
        MatSlideToggleModule,
        MatCardModule,
        FormsModule,
        ReactiveFormsModule,
        MatInputModule,
        MatSelectModule,
        BrowserAnimationsModule,
      ],
      providers: [
        ConfigService,
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

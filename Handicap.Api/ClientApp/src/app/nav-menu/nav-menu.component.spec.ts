import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { OAuthService, OAuthModule, UrlHelperService, OAuthLogger } from 'angular-oauth2-oidc';

import { NavMenuComponent } from './nav-menu.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HttpClient, HttpHeaders, HttpParams, HttpHandler } from '@angular/common/http';

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NavMenuComponent],
      imports: [
        RouterTestingModule,
        TranslateModule.forRoot(),
        MatToolbarModule,
        OAuthModule.forRoot()
      ],
      providers: [
        // OAuthService,
        // UrlHelperService,
        // OAuthLogger,
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

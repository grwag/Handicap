import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatToolbarModule } from '@angular/material';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HttpClient, HttpHandler, HttpClientModule } from '@angular/common/http';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        TranslateModule.forRoot(),
        MatToolbarModule,
        OAuthModule.forRoot(),
        HttpClientModule,
      ],
      declarations: [
        AppComponent,
        NavMenuComponent
      ],
      providers: [
        HttpClient,
        HttpHandler,
        { provide: 'BASE_API_URL', useClass: class { } }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Handicap'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Handicap');
  });

});

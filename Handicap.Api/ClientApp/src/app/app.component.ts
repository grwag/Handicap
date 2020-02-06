import { Component, Inject } from '@angular/core';
import { JwksValidationHandler, AuthConfig } from 'angular-oauth2-oidc';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpClient } from '@angular/common/http';
import { HandicapAuthConfig } from './shared/auth/handicapAuthConfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Handicap';
  baseApiUrl: string;

  constructor(private oauthService: OAuthService, private http: HttpClient, @Inject('BASE_API_URL') baseApiUrl: string) {
    this.baseApiUrl = baseApiUrl;
    this.configure();
  }

  private configure() {
    this.http.get<HandicapAuthConfig>(this.baseApiUrl + 'clientconfig')
      .subscribe(config => {
        const oauthConfig: AuthConfig = {
          issuer: config.issuer,
          redirectUri: config.redirectUri,
          clientId: config.clientId,
          dummyClientSecret: config.clientSecret,
          responseType: config.responseType,
          scope: config.scope,
          postLogoutRedirectUri: config.postLogoutRedirectUri,
        };
        this.oauthService.configure(oauthConfig);
        this.oauthService.setupAutomaticSilentRefresh();
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        this.oauthService.loadDiscoveryDocumentAndTryLogin();
      });
  }

}

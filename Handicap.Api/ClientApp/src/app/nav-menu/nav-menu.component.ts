import { Component, OnInit, AfterViewInit, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, AfterViewChecked {

  isExpanded = false;
  loggedIn$: Observable<boolean>;
  private loggedInBehavior = new BehaviorSubject<boolean>(false);

  constructor(private oauthService: OAuthService, private cdRef: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.loggedIn$ = this.isLoggedIn();
  }

  ngAfterViewChecked() {
    this.loggedIn$ = this.isLoggedIn();
  }

  isLoggedIn() {
    const claims = this.oauthService.getIdentityClaims();
    if (this.oauthService.hasValidAccessToken()) {
      this.loggedInBehavior.next(true);

    } else {
      this.loggedInBehavior.next(false);
    }
    this.cdRef.detectChanges();
    return this.loggedInBehavior.asObservable();
  }

  login() {
    this.oauthService.initCodeFlow();
  }

  logout() {
    this.oauthService.logOut();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: 'https://id.greshawag.com',
  // issuer: 'https://localhost:5001',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin,

  // The SPA's id. The SPA is registered with this id at the auth-server
  // id.greshawag.com
  clientId: '26d33d30-7790-49c2-a544-8ce8e18ebd4a',
  // local identityserver
  // clientId: '6db7ad0e-8f90-4c8f-a191-048dd0253d1c',
  dummyClientSecret: 'secret',
  responseType: 'code',
  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'read read_write openid profile email offline_access',
  postLogoutRedirectUri: 'https://localhost:5001',
};

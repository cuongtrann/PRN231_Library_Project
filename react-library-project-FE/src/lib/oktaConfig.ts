export const oktaConfig = {
  clientId: "0oacvxwz5hg0elxRA5d7",
  issuer: "https://dev-59753842.okta.com/oauth2/default",
  redirectUri: "http://localhost:3000/login/callback",
  scopes: ["openid", "profile", "email"],
  pkce: true,
  disableHttpsCheck: true,
};

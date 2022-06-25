using Catalogo_Blazor.Client.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Catalogo_Blazor.Client.Auth
{
    public class TokenAuthenticationProvider : AuthenticationStateProvider, IAuthorizeService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient http;
        public static readonly string tokenKey = "tokenKey";

        public TokenAuthenticationProvider(IJSRuntime js, HttpClient http)
        {
            this.js = js;
            this.http = http;
        }

        public AuthenticationState NotAuthenticated =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(tokenKey);
            if(string.IsNullOrEmpty(token))
                return NotAuthenticated;

            return CreateAuthenticationState(token);
        }

        public AuthenticationState CreateAuthenticationState(string token)
        {
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims;

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "jwt")));
        }

        public async Task Login(string token)
        {
            await js.SetInLocalStorage(tokenKey, token);
            var authState = CreateAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await js.RemoveFromLocalStorage(tokenKey);
            http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(NotAuthenticated));
        }
    }
}

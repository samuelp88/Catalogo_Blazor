using Catalogo_Blazor.Client;
using Catalogo_Blazor.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<TokenAuthenticationProvider>();
builder.Services.AddScoped<IAuthorizeService, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>()
    );

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>()
    );

await builder.Build().RunAsync();

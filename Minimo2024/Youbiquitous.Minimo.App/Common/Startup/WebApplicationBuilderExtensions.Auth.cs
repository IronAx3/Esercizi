///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Youbiquitous.Minimo.App.Common.Startup;

/// <summary>
/// AUTH methods: cookies, Azure AD, SSO, MFA
/// </summary>
public static partial class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Contains the code to add and configure the cookie-based authentication service 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="loginPath"></param>
    /// <param name="cookieName"></param>
    /// <param name="minutesBeforeExpiration"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupCookieAuthentication(this WebApplicationBuilder builder,
        string loginPath, string cookieName, int minutesBeforeExpiration = 60)
    {
        // Authentication setup
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = cookieName;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(minutesBeforeExpiration);
                options.LoginPath = new PathString(loginPath);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Lax; //SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

        return builder;
    }


    /// <summary>
    /// Contains the code to add and configure the Azure AD authentication service (WTA)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupWtaAzureAdAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication()
            .AddMicrosoftIdentityWebApp(builder.Configuration, 
                configSectionName: "AdB2C:Wta", 
                openIdConnectScheme: AzureAdConstants.WtaOpenIdScheme,
                cookieScheme: AzureAdConstants.WtaTempCookieScheme);

        builder.Services
            .Configure<OpenIdConnectOptions>(AzureAdConstants.WtaOpenIdScheme, options =>
            {
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Add(options.ClientId);
                options.RemoteSignOutPath = "/account/logout";
                options.Events.OnRemoteFailure = async context =>
                {
                    context.Response.Redirect("/");
                    context.HandleResponse();
                };
            });
        return builder;
    }

    /// <summary>
    /// Contains the code to add and configure the Azure AD authentication service (ITF)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupItfAzureAdAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication()
            .AddMicrosoftIdentityWebApp(options =>
                {
                    builder.Configuration.GetSection("AdB2C:Itf").Bind(options);
                    options.ResponseType = OpenIdConnectResponseType.IdToken;
                    options.Scope.Add(options.ClientId);
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Events = BuildOpenIdConnectOptionsEvents(options);
                },
                openIdConnectScheme: AzureAdConstants.ItfOpenIdScheme, 
                cookieScheme: AzureAdConstants.ItfTempCookieScheme);
        return builder;
    }


    #region PRIVATE

    /// <summary>
    /// Further configuration of the redirector
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    private static OpenIdConnectEvents BuildOpenIdConnectOptionsEvents(OpenIdConnectOptions options)
    {
        return new OpenIdConnectEvents 
        {
            OnRedirectToIdentityProvider = context =>
            {
                options.Scope.Add(options.ClientId);
                options.Scope.Add("openid");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                context.ProtocolMessage.SetParameter("clientId", "itf-public-person-portal");

                return Task.CompletedTask;
            }
        };
    }

    #endregion
}

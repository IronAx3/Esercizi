///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Rewrite;
using Youbiquitous.Minimo.Services.System;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.Shared.Exceptions;

namespace Youbiquitous.Minimo.App.Common.Startup;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Contains the code to configure error handling
    /// </summary>
    /// <param name="app"></param>
    /// <param name="errorUrl"></param>
    /// <param name="exceptionMode"></param>
    /// <returns></returns>
    public static WebApplication SetupErrorHandling(this WebApplication app,
        string errorUrl, ExceptionHandlingMode exceptionMode = ExceptionHandlingMode.Auto)
    {
        var isDevelopment = app.Environment.IsDevelopment();

        // Global error handler
        if (exceptionMode.IsAuto() && isDevelopment || exceptionMode.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler(errorUrl);

        // Handle 404 not-found errors
        app.Use(async (context, next) =>
        {
            await next();       // let it go
            if (context.Response.StatusCode == 404)
            {
                context.Request.Path = errorUrl;
                await next();
            }
        });

        return app;
    }

    /// <summary>
    /// Contains configuration for cookie policy and other HTTP security-related aspects
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupSecurity(this WebApplication app)
    {
        app.UseCookiePolicy();
        app.UseHsts();
        app.UseHttpsRedirection();

        // Append security headers
        app.Use(async (context, next) =>
        {
            if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
                context.Response.Headers.Append("X-Frame-Options", "DENY");
            if (!context.Response.Headers.ContainsKey("X-Xss-Protection"))
                context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
            if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
                context.Response.Headers.Append("Referrer-Policy", "no-referrer");

            await next();
        });

        return app;
    }

    /// <summary>
    /// Just for ITF AD
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupMoreServices(this WebApplication app)
    {
        return app;
    }

    /// <summary>
    /// Contains the code to set static files and basic routing
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupRouting(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAntiforgery();

        return app;
    }

    /// <summary>
    /// Contains the code to recognize controller methods
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupControllerEndpoints(this WebApplication app)
    {
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");
        });
#pragma warning restore ASP0014

        return app;
    }

    /// <summary>
    /// Contains the code to setup database(s)
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupDatabase(this WebApplication app)
    {
        var settings = app.Services.GetService<MinimoSettings>();
        if (settings == null)
            return app;

        SystemService.ConfigureDatabases(settings);
        return app;
    }

    /// <summary>
    /// Contains the code to setup UI localization
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetupLocalization(this WebApplication app)
    {
        var provider = new CookieRequestCultureProvider
        {
            CookieName = MinimoSettings.CultureCookieName
        };

        var settings = app.Services.GetService<MinimoSettings>();
        if (settings == null)
            return app;
            
        // Localization (the system will figure out the initial language)
        var supportedCultures = settings.Localizer.GetSupportedCultures();
        var defaultCulture = supportedCultures.Any() 
            ? supportedCultures[0] 
            : settings.Localizer.DefaultCulture();
        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = supportedCultures,      // Formatting numbers, dates, etc.
            SupportedUICultures = supportedCultures,    // UI strings that we have localized
        };
        options.RequestCultureProviders.Clear();
        options.RequestCultureProviders.Add(provider);
        app.UseRequestLocalization(options);
        return app;
    }
}

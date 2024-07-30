///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.App.Common.Startup;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.Shared.Exceptions;

namespace Youbiquitous.Minimo.App;

/// <summary>
/// Main class of the web-app
/// </summary>
public class Program
{
    /// <summary>
    /// Root entry point in the web app
    /// </summary>
    /// <param name="args">CLI arguments (if any)</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .SetupSettings<MinimoSettings>()
            .SetupCookieAuthentication("/account/login", MinimoSettings.AuthCookieName)
            //.SetupWtaAzureAdAuthentication()
            //.SetupItfAzureAdAuthentication()
            .SetupAdditionalServices()
            .SetupLoggers()
            .SetupMvc();

        // Build the app instance
        var app = builder
            .Build()
            .SetupLocalization()
            //.SetupErrorHandling("/app/error", ExceptionHandlingMode.Production)
            .SetupSecurity()
            .SetupMoreServices()     
            .SetupDatabase();

        // Auth stuff
        app.UseAuthentication();

        // Endpoints setup
        app.SetupRouting()
            .SetupControllerEndpoints();

        app.Run();
    }
}

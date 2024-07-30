///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Youbiquitous.Martlet.Mvc.Binders;

namespace Youbiquitous.Minimo.App.Common.Startup;

public static partial class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Contains the code to set up the configuration tree for application settings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="fileName">JSON extension assumed</param>
    /// <param name="devExtension"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupSettings<T>(this WebApplicationBuilder builder,
        string fileName = "app-settings", string devExtension = "-dev")
        where T : class, new()
    {
        var env = builder.Environment;
        var settingsFileName = env.IsDevelopment()
            ? $"{fileName}{devExtension}.json"
            : $"{fileName}.json";

        builder.Configuration
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile(settingsFileName, optional: true)
            .AddEnvironmentVariables();

        var settings = new T();
        builder.Configuration.Bind(settings);
        builder.Services.AddSingleton(settings);
        return builder;
    }

    /// <summary>
    /// Contains the code to add and configure loggers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupLoggers(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;

        // Clear out default loggers added by the default web host builder (program.cs)
        builder.Services.AddLogging(config =>
        {
            config.ClearProviders();
            if (env.IsDevelopment())
            {
                config.AddDebug();
                config.AddConsole();
            }
        });

        return builder;
    }

    /// <summary>
    /// Contains the code to add further miscellaneous runtime services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupAdditionalServices(this WebApplicationBuilder builder)
    {
        // HTTP accessor
        builder.Services.AddHttpContextAccessor();

        return builder;
    }

    /// <summary>
    /// Contains the code to setup MVC controllers (at least for login)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetupMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization();
        builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new StringToBooleanModelBinderProvider());
                options.ModelBinderProviders.Insert(1, new NumericModelBinderProvider());
            })
            .AddMvcLocalization()
            .AddRazorRuntimeCompilation();

        // Increases the max number of input fields in forms
        builder.Services.Configure<FormOptions>(
            options => options.ValueCountLimit = 5000);
        return builder;
    }
}

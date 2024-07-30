///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.Persistence;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.Services.System;

public partial class SystemService : ApplicationServiceBase
{
    public SystemService(MinimoSettings settings) : base(settings) { }
    

    /// <summary>
    /// Ensure all required databases exist, are properly designed and initialized
    /// </summary>
    /// <param name="settings"></param>
    public static void ConfigureDatabases(MinimoSettings settings)
    {
        MinimoDatabaseInitializer.Initialize(settings.Secrets.MinimoDatabase.Get());
    }
}
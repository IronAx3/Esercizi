///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.Services;

public class ApplicationServiceBase
{
    public ApplicationServiceBase(MinimoSettings settings)
    {
        Settings = settings;
    }

    public MinimoSettings Settings { get; }
}
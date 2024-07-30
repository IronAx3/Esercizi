///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.Settings.Core;

public class MinimoSettings
{
    public const string AuthCookieName = "MINIMO.Auth";
    public const string CultureCookieName = "MINIMO.Culture";
    public const string AppName = "MINIMO";

    public MinimoSettings()
    {
        General = new GeneralSettings();
        Secrets = new SecretsSettings();
        Localizer = new LocalizerSettings();
    }

    public GeneralSettings General { get; set; }
    public SecretsSettings Secrets { get; set; }
    public RunSettings Run { get; set; }
    public LocalizerSettings Localizer { get; set; }
   
}
///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.App.Common.Startup;

public static class AzureAdConstants
{
    /// <summary>
    /// Constants to implement custom Azure AD B2C authentication
    /// (WTA portal)
    /// </summary>
    public const string WtaOpenIdScheme = "AdB2C_Wta";
    public const string WtaTempCookieScheme = "WTA-TEMP";

    /// <summary>
    /// Constants to implement custom Azure AD B2C authentication
    /// (ITF portal)
    /// </summary>
    public const string ItfOpenIdScheme = "AdB2C_Itf";
    public const string ItfTempCookieScheme = "ITF-TEMP";
}
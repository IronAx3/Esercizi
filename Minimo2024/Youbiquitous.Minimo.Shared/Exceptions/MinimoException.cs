///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Shared.Extensions;

namespace Youbiquitous.Minimo.Shared.Exceptions;

/// <summary>
/// Base exception class to use within the application
/// </summary>
public class MinimoException : Exception
{
    public MinimoException() : base(DefaultMessage())
    {
        RecoveryLinks = new List<RecoveryLink>();
        ContinueUrl = "/";
    }
    public MinimoException(string message) : base(message)
    {
        RecoveryLinks = new List<RecoveryLink>();
        ContinueUrl = "/";
    }
    public MinimoException(Exception exception) : this(exception.Message)
    {
        ContinueUrl = "/";
        ThrownUncontrolled = true;
    }

    /// <summary>
    /// Whether it is NOT a hospi-specific exception--dont show detailed messages to users
    /// </summary>
    public bool ThrownUncontrolled { get; private set; }

    /// <summary>
    /// Displayed only to SYSTEM accounts
    /// </summary>
    public string InternalMessage { get; private set; }

    /// <summary>
    /// Return URL to continue
    /// </summary>
    public string ContinueUrl { get; private set; }

    /// <summary>
    /// Collection of recovery links to show in the UI
    /// </summary>
    public List<RecoveryLink> RecoveryLinks { get; }

    /// <summary>
    /// Default message for the exception
    /// </summary>
    public static string DefaultMessage()
    {
        var msg = AppErrors.Err_SomethingWentWrong;
        return msg;
    }

    /// <summary>
    /// Select verbose or standard error message
    /// </summary>
    /// <param name="verbose"></param>
    /// <returns></returns>
    public string DisplayMessage(bool verbose = false)
    {
        return verbose ? this.FullMessage() : Message;
    }

    /// <summary>
    /// Save the internal message
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public MinimoException AddInternal(string msg)
    {
        if (!string.IsNullOrWhiteSpace(msg))
            InternalMessage = msg;
        return this;
    }

    /// <summary>
    /// Save the internal message
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public MinimoException AddContinueUrl(string url)
    {
        ContinueUrl = string.IsNullOrWhiteSpace(url) ? "/" : url;
        return this;
    }

    /// <summary>
    /// Add a new recovery link through its list of pieces of information
    /// </summary>
    /// <param name="text"></param>
    /// <param name="url"></param>
    /// <param name="blank"></param>
    /// <returns></returns>
    public MinimoException AddRecoveryLink(string text, string url, bool blank = false)
    {
        RecoveryLinks.Add(new RecoveryLink(text, url, blank));
        return this;
    }

    /// <summary>
    /// Add a new recovery link as an object
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public MinimoException AddRecoveryLink(RecoveryLink link)
    {
        RecoveryLinks.Add(link);
        return this;
    }
}





/// <summary>
/// Wrapper class for links to show in the recovery UI
/// </summary>
public class RecoveryLink
{
    public RecoveryLink(string text, string url, bool blank = false)
    {
        Text = text;
        Url = url;
        Blank = blank;
    }

    public string Text { get; }
    public string Url { get; }
    public bool Blank { get; }
}
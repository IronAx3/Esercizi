///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Services.Security.Password;
using Youbiquitous.Martlet.Services.Security;
using Youbiquitous.Minimo.DomainModel.Account;
using Youbiquitous.Minimo.Infrastructure.Services.Authentication;
using Youbiquitous.Minimo.Persistence.Repositories;
using Youbiquitous.Minimo.Resources;
using Youbiquitous.Minimo.Settings.Core;
using Youbiquitous.Minimo.Shared.Auth;
using Youbiquitous.Minimo.ViewModels.Account;

namespace Youbiquitous.Minimo.Services.Auth;

public class AuthService : ApplicationServiceBase
{
    private readonly IPasswordService _passwordService;
    private readonly IFileService _fileService;
    private readonly AuthenticationChain _chain;

    public AuthService(MinimoSettings settings) : base(settings)
    {
        _passwordService = new DefaultPasswordService();
        _fileService = new DefaultFileService(Settings.General.TemplateRoot);
        _chain = new AuthenticationChain()
            .Add(new MinimoAuthenticationProvider())
            .Add(new NopAuthenticationProvider());
    }

    /// <summary>
    /// Locate user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static UserAccount FindUser(string email)
    {
        return UserRepository.FindByEmail(email);
    }

    /// <summary>
    /// Try to locate a matching regular account (whether member or admin) 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public AuthenticationResponse TryAuthenticate(LoginInput input)
    {
        var response = _chain.Evaluate(input.UserName, input.Password);
        return response;
    }

    /// <summary>
    /// Try to locate a matching regular account (whether member or admin) 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public AuthenticationResponse TryAuthenticateInternal(LoginInput input)
    {
        var c = new MinimoAuthenticationProvider();
        return c.ValidateCredentialsInternal(input.UserName, null, skipPasswordCheck: true);
    }

    /// <summary>
    /// Enables password reset by sending a link
    /// </summary>
    /// <param name="email"></param>
    /// <param name="server"></param>
    /// <param name="lang"></param>
    /// <returns></returns>
    public CommandResponse TrySendLinkForPasswordReset(string email, string server, string lang)
    {
        if (email.IsNullOrWhitespace())
            return CommandResponse.Fail();

        var userRepository = new UserRepository(string.Empty);
        var user = userRepository.MarkForPasswordReset(email);
        if (user == null)
            return CommandResponse.Fail().AddMessage(AppMessages.Info_Failed);

        // Prepare email with reset link
        var template = "email_password_reset.txt";
        var messageFormat = _fileService.Load(template, lang);
        var resetLink = $"{server}/account/reset/{user.PasswordResetToken}";
        var message = string.Format(messageFormat, resetLink);

        // Prepare and send email
        //var output = EmailExtensionsSend(email, null, AppStrings.Label_PasswordReset, message);
        return CommandResponse.Ok();
    }

    /// <summary>
    /// Reset the user password  
    /// </summary>
    /// <param name="token"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public CommandResponse TryResetPassword(string token, string newPassword)
    {
        if (token.IsNullOrWhitespace() || newPassword.IsNullOrWhitespace() || newPassword.Length < Settings.General.MinPasswordLength)
            return CommandResponse.Fail().AddMessage(AppMessages.Err_InvalidPassword);

        var userRepository = new UserRepository(string.Empty);
        return userRepository.ResetPasswordByToken(token, newPassword);
    }

    /// <summary>
    /// Proxy to the password token checker method
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public CommandResponse CheckPasswordToken(string token)
    {
        var user = UserRepository.FindByPasswordToken(token);
        return user == null ? CommandResponse.Fail() : CommandResponse.Ok();
    }

}
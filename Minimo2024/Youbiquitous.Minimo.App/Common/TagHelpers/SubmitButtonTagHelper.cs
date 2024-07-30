///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//


using Microsoft.AspNetCore.Razor.TagHelpers;
using Youbiquitous.Martlet.Core.Extensions;

namespace Youbiquitous.Minimo.App.Common.TagHelpers;

/// <summary>
/// Razor tag helper submit buttons 
/// </summary>
[HtmlTargetElement("submit-button")]
public class SubmitButtonTagHelper : TagHelper
{
    const string DefaultClass = "btn btn-primary px-5";
    const string DefaultGeneralError = "???";
    private const int DefaultTimeoutInSecs = 2;

    public SubmitButtonTagHelper()
    {
        MessageTimeout = DefaultTimeoutInSecs;
        Action = PostbackAction.ShowFeedbackAndContinue;
        GeneralErrorText = DefaultGeneralError;
        FeedbackFailureClass = "text-danger";
        FeedbackSuccessClass = "text-success";
    }

    /// <summary>
    /// Expression denoting the validation rules to apply before submitting
    /// </summary>
    public string ValidationExpression { get; set; }

    /// <summary>
    /// CSS selector of the element to display any feedback message (validation fail, response of submission)
    /// </summary>
    public string FeedbackElement { get; set; }

    /// <summary>
    /// Text to display in case of failed validation 
    /// </summary>
    public string FeedbackText { get; set; }

    /// <summary>
    /// CSS class to apply in case of positive outcome and feedback to show
    /// </summary>
    public string FeedbackSuccessClass { get; set; }

    /// <summary>
    /// CSS class to apply in case of negative outcome and feedback to show
    /// </summary>
    public string FeedbackFailureClass { get; set; }

    /// <summary>
    /// Message to show in case of unhandled exceptions or general failure
    /// </summary>
    public string GeneralErrorText { get; set; }

    /// <summary>
    /// Action to take once the form is posted
    /// </summary>
    public PostbackAction Action { get; set; }

    /// <summary>
    /// Optional parameter to pass when executing the final action
    /// </summary>
    public string ActionParameter { get; set; }

    /// <summary>
    /// Seconds to leave any feedback message on
    /// </summary>
    public int MessageTimeout { get; set; }

    /// <summary>
    /// Internal HTML factory 
    /// </summary>
    /// <param name="context">Custom markup tree</param>
    /// <param name="output">HTML final tree</param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var css = output.Attributes["class"]?.Value.ToString();
        var secs = MessageTimeout < DefaultTimeoutInSecs ? DefaultTimeoutInSecs : MessageTimeout;
        var general = GeneralErrorText.IsNullOrWhitespace() ? DefaultGeneralError : GeneralErrorText;

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;
        
        output.Attributes.SetAttribute("type", "button");
        output.Attributes.SetAttribute("onclick", $"__formSubmitContent(this, {secs});");
        if (string.IsNullOrWhiteSpace(css))
            output.Attributes.SetAttribute("class", DefaultClass);

        if (!string.IsNullOrWhiteSpace(FeedbackElement))
            output.Attributes.SetAttribute("data-ui-feedback", FeedbackElement);
        if (!string.IsNullOrWhiteSpace(FeedbackText))
            output.Attributes.SetAttribute("data-ui-error", FeedbackText);
        if (!string.IsNullOrWhiteSpace(ValidationExpression))
            output.Attributes.SetAttribute("data-ui-validation", ValidationExpression);
        if (!string.IsNullOrWhiteSpace(GeneralErrorText))
            output.Attributes.SetAttribute("data-ui-general-error", general);

        // More data-* attributes
        output.Attributes.SetAttribute("data-post-action", $"{Action}".ToLower());
        output.Attributes.SetAttribute("data-post-action-param", ActionParameter);
        output.Attributes.SetAttribute("data-ui-css-success", FeedbackSuccessClass);
        output.Attributes.SetAttribute("data-ui-css-failure", FeedbackFailureClass);
    }
}


/// <summary>
/// Actions supported after the form is posted
/// 
/// ShowFeedback => displays any text response from the form action
/// Reload       => reloads existing page
/// Redirect     => jumps to specified URL
/// Function     => executes custom JS code
/// </summary>
public enum PostbackAction
{
    ShowFeedbackAndContinue = 0,
    ShowFeedback = 1,
    Reload = 2,
    Redirect = 3,
    Function = 4
}
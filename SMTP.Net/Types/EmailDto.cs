namespace SMTP.Net.Types;

/// <summary>
/// Represents the data transfer object for an email message
/// </summary>
public class EmailDto
{
    /// <summary>
    /// Gets or sets the date and time when the email was sent
    /// </summary>
    public DateTime? Date { get; set; }
    /// <summary>
    /// Gets or sets the subject line of the email
    /// </summary>
    public string Subject { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the senders email address
    /// </summary>
    public string From { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the primary recipient(s) of the email
    /// </summary>
    public string To { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the carbon copy recipient(s) of the email
    /// </summary>
    public string Cc { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the blind carbon copy recipient(s) of the email
    /// </summary>
    public string Bcc { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the email headers as key-value pairs
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new();
    /// <summary>
    /// Gets or sets the HTML formatted body of the email
    /// </summary>
    public string HtmlBody { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the plain text body of the email
    /// </summary>
    public string TextBody { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the raw MIME message as a string
    /// </summary>
    public string RawMessage { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the list of individual mail parts
    /// </summary>
    public List<EmailPartDto> Parts { get; set; } = new();
}
namespace SMTP.Net.Types;

/// <summary>
/// Represents a part of an email message, such as text, HTML content, or attachments
/// </summary>
public class EmailPartDto
{
    /// <summary>
    /// Gets or sets the content type of the email part, such as "text/plain"
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the content of the email part
    /// </summary>
    public string Content { get; set; } = string.Empty;
    /// <summary>
    /// Gets  or sets whether the email part is an attachment
    /// </summary>
    public bool IsAttachment { get; set; } = false;
    /// <summary>
    /// Gets or sets the filename of the attachment if applicable
    /// </summary>
    public string FileName { get; set; } = string.Empty;
}

using MimeKit;
using SMTP.Net.Types;

namespace SMTP.Net.Services;

/// <summary>
/// Provides utility methods for processing and extracting data from email messages
/// </summary>
public class EmailHelperService
{
    /// <summary>
    /// Retrieves the raw MIME message as a UTF-8 encoded string
    /// </summary>
    /// <param name="message">The email message to convert</param>
    /// <returns>The raw email message as a string</returns>
    public string GetRawMessage(MimeMessage message)
    {
        using var stream = new MemoryStream();
        message.WriteTo(stream);
        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// Extracts and proceses the different parts of an email message
    /// </summary>
    /// <param name="message">The email message to process</param>
    /// <returns>A ist representing the emails parts</returns>
    public List<EmailPartDto> ExtractEmailParts(MimeMessage message)
    {
        var parts = new List<EmailPartDto>();

        if (message.Body is Multipart multipart)
        {
            foreach (var part in multipart)
            {
                parts.Add(ProcessMimeEntity(part));
            }
        }
        else
        {
            parts.Add(ProcessMimeEntity(message.Body));
        }

        return parts;
    }

    /// <summary>
    /// Processes an individual MIME entity and extracts its content
    /// </summary>
    /// <param name="entity">The MIME entity to process</param>
    /// <returns>The extracted details of the email part</returns>
    private static EmailPartDto ProcessMimeEntity(MimeEntity entity)
    {
        var partDto = new EmailPartDto
        {
            ContentType = entity.ContentType.MimeType,
            IsAttachment = entity.IsAttachment
        };

        if (entity is TextPart textPart)
        {
            partDto.Content = textPart.Text;
        }
        else if (entity is MimePart attachmentPart && attachmentPart.Content != null)
        {
            partDto.FileName = attachmentPart.FileName ?? "UnknownAttachment";
            using var memoryStream = new MemoryStream();
            attachmentPart.Content.DecodeTo(memoryStream);
            partDto.Content = Convert.ToBase64String(memoryStream.ToArray()); // Base64 for safe transfer
        }

        return partDto;
    }
}

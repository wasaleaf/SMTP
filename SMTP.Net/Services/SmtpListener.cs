using Microsoft.AspNetCore.SignalR;
using MimeKit;
using SMTP.Net.Hubs;
using SMTP.Net.Types;

namespace SMTP.Net.Services;

/// <summary>
/// Listens for incoming emails using an SMTP server and broadcasts received messages via SignalR
/// </summary>
public class SmtpListener : BackgroundService
{
    private readonly SmtpService smtpService;
    private readonly IHubContext<EmailHub, IEmailHub> hubContext;

    /// <summary>
    /// Subscribes to the SMTP service to handle incoming emails
    /// </summary>
    /// <param name="hub">The SignalR hub context for broadcasting received emails</param>
    public SmtpListener(IHubContext<EmailHub, IEmailHub> hub)
    {
        hubContext = hub;
        smtpService = new SmtpService();
        smtpService.MailReceived += HandleMailReceived;
    }

    /// <summary>
    /// Starts the background service to listen for incoming emails
    /// </summary>
    /// <param name="stoppingToken">A token to monitor service shutdown requests</param>
    /// <returns>A completed task once the service has started</returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = smtpService.StartAsync(stoppingToken);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles incoming emails, converts them into a structured DTO and broadcasts them via SignalR
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="message">The receoved email message</param>
    private void HandleMailReceived(object? sender, MimeMessage message)
    {
        var service = new EmailHelperService();

        var emailDto = new EmailDto()
        {
            Date = message.Date != DateTimeOffset.MinValue ? message.Date.DateTime : null,
            From = message.From.Mailboxes.FirstOrDefault()?.Address ?? "Unknown",
            To = string.Join(", ", message.To.Mailboxes.Select(x => x.Address)),
            Cc = string.Join(", ", message.Cc.Mailboxes.Select(x => x.Address)),
            Bcc = string.Join(", ", message.Bcc.Mailboxes.Select(x => x.Address)),
            Subject = message.Subject ?? "(No Subject)",
            HtmlBody = message.HtmlBody ?? string.Empty,
            TextBody = message.TextBody ?? string.Empty,
            RawMessage = service.GetRawMessage(message),
            Headers = message.Headers.ToDictionary(x => x.Field, x => x.Value),
            Parts = service.ExtractEmailParts(message)
        };

        hubContext.Clients.All.ReceivedMail(emailDto).GetAwaiter().GetResult();
    }
}

using MimeKit;
using SmtpServer;

namespace SMTP.Net.Services;

/// <summary>
/// Uses the SmtpServer library to handle SMTP connections and trigger evernt when emails are received
/// </summary>
public class SmtpService
{
    private readonly SmtpServer.SmtpServer _server;

    /// <summary>
    /// Occurs when an email is received and successfully processed
    /// </summary>
    public event EventHandler<MimeMessage>? MailReceived;

    /// <summary>
    /// Configures the SMTP service and initializes it to listen on the correct server and port 25
    /// </summary>
    public SmtpService()
    {
        var options = new SmtpServerOptionsBuilder()
            .ServerName("0.0.0.0")
            .Port(25)
            .Build();

        var store = new SmtpMessageStore();
        store.MessageReceived += HandleMailReceived;

        var serviceProvider = new SmtpServer.ComponentModel.ServiceProvider();
        serviceProvider.Add(store);

        _server = new SmtpServer.SmtpServer(options, serviceProvider);
    }

    /// <summary>
    /// Starts the SMTP server asynchronously and listens for incoming emails
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _server.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Handles an incoming email message and triggers the mail received event
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="message">The received email message</param>
    private void HandleMailReceived(object? sender, MimeMessage message)
    {
        MailReceived?.Invoke(sender, message);
    }
}

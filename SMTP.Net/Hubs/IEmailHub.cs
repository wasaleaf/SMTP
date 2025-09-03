using SMTP.Net.Types;

namespace SMTP.Net.Hubs;

/// <summary>
/// Defines the SignalR hub interface for real time email notifications
/// </summary>
public interface IEmailHub
{
    /// <summary>
    /// Notifies connected clients when a new email is received
    /// </summary>
    /// <param name="email">The email data transfer object</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public Task ReceivedMail(EmailDto email);
}

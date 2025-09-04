using MimeKit;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System.Buffers;

namespace SMTP.Net.Services;

/// <summary>
/// Implements an SMTP message store that processes and saved incoming email messages
/// </summary>
public class SmtpMessageStore : MessageStore
{
    /// <summary>
    /// Occurs when an email message is received and successfully parsed
    /// </summary>
    public event EventHandler<MimeMessage>? MessageReceived;

    /// <summary>
    /// Saves an incoming SMTP message by reading the buffer and converting it into a MimeMessage
    /// </summary>
    /// <param name="context">The current SMTP session context</param>
    /// <param name="transaction">The SMTP message transation details</param>
    /// <param name="buffer">The raw email message context</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A SmtpResponse indicating the result of the operation</returns>
    public override async Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
    {
        await using var stream = new MemoryStream();

        var position = buffer.GetPosition(0);
        while (buffer.TryGet(ref position, out var memory))
        {
            await stream.WriteAsync(memory, cancellationToken);
        }

        stream.Position = 0;

        var message = await MimeMessage.LoadAsync(stream, cancellationToken);
        MessageReceived?.Invoke(this, message);

        return SmtpResponse.Ok;
    }
}

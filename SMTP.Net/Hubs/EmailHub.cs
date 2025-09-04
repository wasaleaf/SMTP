using Microsoft.AspNetCore.SignalR;

namespace SMTP.Net.Hubs;

/// <summary>
/// Real time hub for handling email related event and notifications
/// </summary>
public class EmailHub : Hub<IEmailHub> { }

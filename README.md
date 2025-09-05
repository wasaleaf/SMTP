# Blazor SMTP Application
A lightweight SMTP server clone inspired by smtp4dev, built as a personal project to explore Blazor and SignalR Hubs.

This project allows you to receive and inspect test emails sent from applications during development.

## Features
- Receive and display incoming SMTP messages
- Real-time message updates via SignalR
- View raw message content (headers, plain text, HTML)
- Configurable SMTP settings (port, etc.)

## Tech Stack
- **Frontend:** Blazor Server  
- **Backend:** ASP.NET Core, SignalR  
- **Containerization:** Docker  
- **Protocol:** SMTP 

## Getting Started
```bash
git clone https://github.com/wasaleaf/SMTP.git
cd SMTP
```

### Configure Ports
Open `deploy.bat` and update the ports if needed
```
SET HTTP_PORT=19280 :: Web UI (Blazor)
SET SMTP_PORT=19281 :: SMTP server
```

### Run
Execute the deployment script:
```bash
deploy.bat
```

This will build and run a new instance of SMTP.Net in Docker at:
- Web UI: http://localhost:%HTTP_PORT%/
- SMTP server: `localhost:%SMTP_PORT%` (default 19281)

## Quick Test
After running `deploy.bat`, you can test the SMTP server by sending a message

### Using PowerShell
```powershell
Send-MailMessage `
  -From "test@local.dev" `
  -To "inbox@local.dev" `
  -Subject "Hello from Blazor SMTP" `
  -Body "This is a test email." `
  -SmtpServer "localhost" `
  -Port 19281
```

### Using telnet
```bash
telnet localhost 19281
```

Then type:
```ruby
HELO localhost
MAIL FROM:<test@local.dev>
RCPT TO:<inbox@local.dev>
DATA
Subject: Test Email

This is a test message.
.
QUIT
```

## Demo
![Blazor SMTP Demo](/assets/demo.gif)
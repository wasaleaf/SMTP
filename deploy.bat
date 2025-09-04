@echo off

echo Building the SMTP.Net Docker image...

docker build -t smtp_net -f SMTP.Net/Dockerfile .
IF %ERRORLEVEL% NEQ 0 (
    echo Docker build failed.
    exit /b %ERRORLEVEL%
)

echo Removing any existing SMTP.Net container...
docker rm -f smtp_net
IF %ERRORLEVEL% NEQ 0 (
    echo Failed to remove exisiting SMTP.Net container or no container found.
    REM Do not exit here; continue to run the new container.
)

echo Running the SMTP.Net Docker container...
docker run -d -p 19280:8080 -p 19281:25 --name smtp_net --restart unless-stopped -e DefaultServer=host.docker.internal -e IsDocker=1 smtp_net
IF %ERRORLEVEL% NEQ 0 (
    echo Docker run failed.
    exit /b %ERRORLEVEL%
)

echo SMTP.Net Docker container is running with port 19280 mapped to port 8080 in the container.
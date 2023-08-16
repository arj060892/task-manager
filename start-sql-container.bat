@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

SET containerName=sql2022instance
SET saPassword=Pa$$w0rd2022
SET maxRetries=12
SET retryInterval=5

:: Run the Docker command
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=!saPassword!" -p 1433:1433 --name !containerName! --hostname !containerName! -d mcr.microsoft.com/mssql/server:2022-latest
echo Docker container !containerName! started.

:: Wait for SQL Server to be ready
SET /A retries=0
:waitloop
if !retries! leq !maxRetries! (
    docker exec !containerName! /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P !saPassword! -Q "SELECT 1" >nul 2>&1
    if errorlevel 1 (
        echo Waiting for SQL Server to be ready...
        timeout /t !retryInterval! /nobreak >nul
        SET /A retries+=1
        goto :waitloop
    ) else (
        echo SQL Server is ready.
    )
)

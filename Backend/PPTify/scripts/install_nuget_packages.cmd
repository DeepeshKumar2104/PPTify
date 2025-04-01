@echo off
echo Installing NuGet packages for PPTify Clean Architecture...

:: ========================
:: Domain Layer (No packages)
:: ========================
echo Skipping Domain Layer - No packages required

:: ========================
:: Application Layer
:: ========================
dotnet add src\Application\PPTify.Application package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add src\Application\PPTify.Application package FluentValidation

:: ========================
:: Infrastructure Layer
:: ========================
dotnet add src\Infrastructure\PPTify.Infrastructure package Pomelo.EntityFrameworkCore.MySql
dotnet add src\Infrastructure\PPTify.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add src\Infrastructure\PPTify.Infrastructure package Microsoft.Extensions.Configuration.Binder
dotnet add src\Infrastructure\PPTify.Infrastructure package MailKit

:: ========================
:: Shared Layer (Optional Utils)
:: ========================
dotnet add src\Shared\PPTify.Shared package Newtonsoft.Json

:: ========================
:: API Layer
:: ========================
dotnet add src\Api\PPTify.Api package Swashbuckle.AspNetCore
dotnet add src\Api\PPTify.Api package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add src\Api\PPTify.Api package Microsoft.AspNetCore.Cors

:: ========================
:: Global Tool (Optional for EF Core Migrations)
:: ========================
dotnet tool install --global dotnet-ef

echo.
echo ✅ All required NuGet packages have been installed successfully!
pause

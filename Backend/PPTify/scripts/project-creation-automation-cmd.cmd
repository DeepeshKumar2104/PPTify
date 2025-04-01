@echo off
cd /d %~dp0
echo.
echo ✅ Starting full setup for PPTify...

:: ========== Create Solution ==========
dotnet new sln -n PPTify

:: ========== Create Main Projects ==========
dotnet new webapi -n PPTify.Api -o src\Api\PPTify.Api
dotnet new classlib -n PPTify.Application -o src\Application\PPTify.Application
dotnet new classlib -n PPTify.Domain -o src\Domain\PPTify.Domain
dotnet new classlib -n PPTify.Infrastructure -o src\Infrastructure\PPTify.Infrastructure
dotnet new classlib -n PPTify.Shared -o src\Shared\PPTify.Shared

:: ========== Add Projects to Solution ==========
dotnet sln add src\Api\PPTify.Api\PPTify.Api.csproj
dotnet sln add src\Application\PPTify.Application\PPTify.Application.csproj
dotnet sln add src\Domain\PPTify.Domain\PPTify.Domain.csproj
dotnet sln add src\Infrastructure\PPTify.Infrastructure\PPTify.Infrastructure.csproj
dotnet sln add src\Shared\PPTify.Shared\PPTify.Shared.csproj

:: ========== Add References ==========
dotnet add src\Application\PPTify.Application\PPTify.Application.csproj reference src\Domain\PPTify.Domain\PPTify.Domain.csproj

dotnet add src\Infrastructure\PPTify.Infrastructure\PPTify.Infrastructure.csproj reference src\Application\PPTify.Application\PPTify.Application.csproj
dotnet add src\Infrastructure\PPTify.Infrastructure\PPTify.Infrastructure.csproj reference src\Domain\PPTify.Domain\PPTify.Domain.csproj
dotnet add src\Infrastructure\PPTify.Infrastructure\PPTify.Infrastructure.csproj reference src\Shared\PPTify.Shared\PPTify.Shared.csproj

dotnet add src\Api\PPTify.Api\PPTify.Api.csproj reference src\Application\PPTify.Application\PPTify.Application.csproj
dotnet add src\Api\PPTify.Api\PPTify.Api.csproj reference src\Infrastructure\PPTify.Infrastructure\PPTify.Infrastructure.csproj
dotnet add src\Api\PPTify.Api\PPTify.Api.csproj reference src\Shared\PPTify.Shared\PPTify.Shared.csproj

:: ========== Create Test Projects ==========
dotnet new xunit -n PPTify.Tests.Unit -o tests\PPTify.Tests.Unit
dotnet new xunit -n PPTify.Tests.Integration -o tests\PPTify.Tests.Integration
dotnet new xunit -n PPTify.Tests.Functional -o tests\PPTify.Tests.Functional

dotnet sln add tests\PPTify.Tests.Unit\PPTify.Tests.Unit.csproj
dotnet sln add tests\PPTify.Tests.Integration\PPTify.Tests.Integration.csproj
dotnet sln add tests\PPTify.Tests.Functional\PPTify.Tests.Functional.csproj

:: ========== Create Dummy Projects for docs and scripts ==========
dotnet new classlib -n PPTify.Docs -o docs\PPTify.Docs
dotnet sln add docs\PPTify.Docs\PPTify.Docs.csproj

dotnet new classlib -n PPTify.Scripts -o scripts\PPTify.Scripts
dotnet sln add scripts\PPTify.Scripts\PPTify.Scripts.csproj

:: ========== Create Empty Files in Subfolders ==========

:: === API ===
cd src\Api\PPTify.Api
mkdir Controllers && type nul > Controllers\Controllers.cs
mkdir Extensions && type nul > Extensions\Extensions.cs
mkdir Middleware && type nul > Middleware\Middleware.cs
mkdir Filters && type nul > Filters\Filters.cs
mkdir Properties && type nul > Properties\Properties.cs
cd ..\..\..\

:: === APPLICATION ===
cd src\Application\PPTify.Application
mkdir Common && type nul > Common\Common.cs
mkdir Behaviors && type nul > Behaviors\Behaviors.cs
mkdir Contracts && type nul > Contracts\Contracts.cs
mkdir Exceptions && type nul > Exceptions\Exceptions.cs
mkdir Services && type nul > Services\Services.cs
mkdir Features
cd Features
mkdir ExampleFeature
cd ExampleFeature
mkdir Commands && type nul > Commands\Commands.cs
mkdir Queries && type nul > Queries\Queries.cs
mkdir Handlers && type nul > Handlers\Handlers.cs
mkdir Validators && type nul > Validators\Validators.cs
cd ..\..\..\..\..\

:: === DOMAIN ===
cd src\Domain\PPTify.Domain
mkdir Entities && type nul > Entities\Entities.cs
mkdir ValueObjects && type nul > ValueObjects\ValueObjects.cs
mkdir Enums && type nul > Enums\Enums.cs
mkdir Exceptions && type nul > Exceptions\Exceptions.cs
mkdir Events && type nul > Events\Events.cs
mkdir Interfaces && type nul > Interfaces\Interfaces.cs
mkdir Common && type nul > Common\Common.cs
cd ..\..\..\

:: === INFRASTRUCTURE ===
cd src\Infrastructure\PPTify.Infrastructure
mkdir Persistence
cd Persistence
mkdir DbContexts && type nul > DbContexts\DbContexts.cs
mkdir Repositories && type nul > Repositories\Repositories.cs
mkdir Configurations && type nul > Configurations\Configurations.cs
mkdir Migrations && type nul > Migrations\Migrations.cs
cd ..
mkdir Services
cd Services
mkdir EmailService && type nul > EmailService\EmailService.cs
mkdir StorageService && type nul > StorageService\StorageService.cs
cd ..
mkdir Authentication
cd Authentication
mkdir Jwt && type nul > Jwt\Jwt.cs
cd ..\..\..\..\

:: === SHARED ===
cd src\Shared\PPTify.Shared
mkdir Helpers && type nul > Helpers\Helpers.cs
mkdir Constants && type nul > Constants\Constants.cs
mkdir Extensions && type nul > Extensions\Extensions.cs
mkdir Utilities && type nul > Utilities\Utilities.cs
cd ..\..\..\

:: === DOCS (non-project folders) ===
mkdir docs\architecture
mkdir docs\api-documentation
mkdir docs\setup-guides
mkdir docs\usage-guides

:: === ROOT FILES ===
type nul > docker-compose.yml
type nul > .gitignore
type nul > README.md

echo.
echo ✅ ALL DONE! Full solution, projects, structure, and visibility in Solution Explorer is ready!
pause
exit /b
REM End of script   
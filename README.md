# Guruvani

Full-stack .NET + React project scaffold

## What was created
- ASP.NET Core Web API (src/Guruvani.Api)
- Project layers: Domain, Application, Infrastructure
- EF Core configured for MySQL (Pomelo)
- xUnit test project (tests/Guruvani.Api.Tests)
- Dockerfile for the API and `docker-compose.yml` (MySQL + API + Adminer)
- GitHub Actions CI workflow (build & tests)

## Quick start (dev)
1. Start MySQL and API using Docker Compose:

   docker-compose up --build

   The API will be available at http://localhost:5000

2. To run migrations locally (install EF tools first):

   dotnet tool install --global dotnet-ef
   cd src/Guruvani.Infrastructure
   dotnet ef migrations add InitialCreate -o Migrations -p ../Guruvani.Infrastructure -s ../Guruvani.Api
   dotnet ef database update -p ../Guruvani.Infrastructure -s ../Guruvani.Api

## Notes
- Connection string is set in `src/Guruvani.Api/appsettings.Development.json` and can be overridden with the `DEFAULT_CONNECTION` environment variable.
- Authentication is scaffolded as a placeholder (JWT). Configure proper signing and validation in `Program.cs`.

## Next steps
- Add frontend scaffold (React + TypeScript)
- Implement domain models, repos, and API controllers
- Add CI steps for linting and frontend tests


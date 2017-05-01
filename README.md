# dotnet-summit-2017-dotnet-core

Sample for the half-day ASP.NET Core workshop at .NET Summit 2017 in Munich. Slides can be found on [Speaker Deck](https://speakerdeck.com/manuelrauber/cross-plattform-serveranwendungen-mit-asp-dot-net-core).

## Prerequisites

* IDE like VS 2017
* .NET Core 1.1
* For using EntityFramework Core either MS SQL or PostgresSQL

## Build & Run

You can either run it via Visual Studio directly or use the following commands within the the folder `src/SampleApplication/SampleApplication`: 

* `dotnet restore` to restore all NuGet packages
* `dotnet run` to start

The console output shows, on which port the application is hosted. Per default:

* Windows: `http://localhost:50685`
* Mac: `http://localhost:5000`

To access swagger:

* Swagger.json: `[BaseUrl]/api/swagger/v1/swagger.json`
* Swagger UI: `[BaseUrl]/api/swagger`

## Storage Engine 

The sample comes with an InMemory storage and an EntityFramework Core based storage (using either MS SQL or Postgres). 
To change, please take a look at the `Startup.cs` file, locate `ConfigureServices` and follow the instructions to change from InMemory to EntityFramework Core.

If you want to change which database provider is used, take a look at the `appSettings.json` and find the `Database` object. Use `mssql` for MS SQL and `postgres` for PostgreSQL. You may also need to change the connection string for your own environment.

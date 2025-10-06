# Nowfeeds

Nowfeeds is a modular .NET 8 application that aggregates local weather, news, and social feeds. It is designed with clean architecture principles, supports dependency injection with Autofac, and is ready for scalable caching, logging, and metrics solutions.

## Features
- Aggregates weather, news, and social feeds for a given city
- Clean separation of Application, Infrastructure, Domain, and API layers
- Extensible caching (in-memory by default, Redis-ready, distributed cache support)
- External API integration (OpenWeatherMap, Twitter)
- Exception handling middleware
- AutoMapper for DTO mapping
- Serilog for structured logging (file, Elasticsearch-ready)
- Health checks endpoint
- Metrics collection and reporting for external service calls
- Swagger/OpenAPI documentation
- xUnit test project for infrastructure and application services

## Project Structure
- **Nowfeeds.Api**: ASP.NET Core Web API, controllers, middleware, API models, startup configuration, health checks, and Swagger
- **Nowfeeds.Application**: Application logic, interfaces, DTOs, business rules, and shared extensions
- **Nowfeeds.Infrastructure**: External service integrations, cache implementations, decorators, metrics, and configuration
- **Nowfeeds.Domain**: Core domain models and value objects
- **Nowfeeds.Test**: xUnit test project for unit and integration tests

## Getting Started

### Prerequisites
- .NET 8 SDK
- (Optional) Redis for distributed caching
- (Optional) Elasticsearch for log aggregation

### Configuration
- Update `appsettings.json` and `appsettings.Development.json` with your API keys and service endpoints for OpenWeatherMap and Twitter.
- Configure the `InfrastructureConfiguration` section for external services and cache settings.
- Configure Serilog in `appsettings.json` for file or Elasticsearch logging.
- For Redis, set the `ConnectionStrings:Redis` value.

### Build and Run
```sh
# Restore dependencies
 dotnet restore

# Build the solution
 dotnet build

# Run the API
 dotnet run --project Nowfeeds.Api
```

### API Documentation
- Swagger UI is available at `/swagger` when running in Development mode.

## Caching
- The app uses a generic `ICacheService` interface.
- Default implementation is in-memory (`InMemoryCacheService`).
- Distributed cache (`CacheService`) supports Redis or SQL Server via `IDistributedCache`.
- To use Redis, configure `AddStackExchangeRedisCache` in `Program.cs` and update DI registration.

## Logging
- Serilog is configured for console and file logging by default.
- Logs are written to `logs/log-.txt` with daily rolling.
- To write logs in Elasticsearch format, use the `Serilog.Formatting.Elasticsearch` package and configure the file sink in `Program.cs`.

## Health Checks
- Health check endpoint is available at `/health`.
- Add custom or external service checks in `Program.cs` as needed.

## Metrics
- HTTP client calls to external services are decorated to record metrics (request count, success/failure, response time, etc.).
- Metrics are stored in cache and can be retrieved via the metrics service.
- Metrics endpoint: `/api/Metrics`

## Testing
- The `Nowfeeds.Test` project contains xUnit tests for infrastructure and application services.
- To run tests:
```sh
dotnet test
```

## Dependency Injection
- Autofac is used as the DI container.
- Modules are registered in `Program.cs`.
- Decorators are used for cross-cutting concerns like metrics.

## Exception Handling
- Custom middleware (`ExceptionsMiddleware`) handles validation and general exceptions, returning structured API responses.

## Extending the App
- Add new external services by implementing interfaces in `Nowfeeds.Application` and providing concrete classes in `Nowfeeds.Infrastructure`.
- Add new API endpoints in `Nowfeeds.Api.Controllers` and map results to API models using AutoMapper.
- Add new health checks or metrics as needed.

## Usage Example
Example request to get local feeds:
```
GET /api/feeds/getlocalfeeds?city=Thessaloniki
```


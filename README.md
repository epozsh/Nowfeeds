# Nowfeeds

Nowfeeds is a modular .NET 8 application that aggregates local weather, news, and social feeds. It is designed with clean architecture principles, supports dependency injection with Autofac, and is ready for scalable caching and logging solutions.

## Features
- Aggregates weather, news, and social feeds for a given city
- Clean separation of Application, Infrastructure, Domain, and API layers
- Extensible caching (in-memory by default, Redis-ready)
- External API integration (OpenWeatherMap, Twitter)
- Exception handling middleware
- AutoMapper for DTO mapping
- Serilog for structured logging
- Swagger/OpenAPI documentation

## Project Structure
- **Nowfeeds.Api**: ASP.NET Core Web API, controllers, middleware, API models, and startup configuration
- **Nowfeeds.Application**: Application logic, interfaces, DTOs, and business rules
- **Nowfeeds.Infrastructure**: External service integrations, cache implementations, and configuration
- **Nowfeeds.Domain**: Core domain models and value objects

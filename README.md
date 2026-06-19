# SupplyChainX

SupplyChainX is a modern Supply Chain Visibility Platform built with ASP.NET Core Web API. The system enables end-to-end product tracking from suppliers to warehouses, distributors, and retailers while providing secure authentication and scalable architecture.

## Features

* JWT Authentication and Authorization
* Role-Based Access Control
* Supplier Management
* Warehouse Management
* Product Tracking
* DTO-Based API Structure
* Entity Framework Core Integration
* SQL Server Database
* Serilog Logging
* Clean Architecture

## Tech Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Authentication
* Serilog
* Swagger / OpenAPI

## Project Structure

```
SupplyChainX
├── Controllers
├── Data
├── DTOs
├── Enums
├── Helpers
├── Models
├── Services
├── Program.cs
├── appsettings.json
└── SupplyChainX.csproj
```

## Getting Started

### Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/SupplyChainX.git
```

### Navigate to the project

```bash
cd SupplyChainX
```

### Restore dependencies

```bash
dotnet restore
```

### Apply migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

## Future Improvements

* Email Verification
* Password Reset
* Refresh Tokens
* Real-Time Notifications
* Analytics Dashboard
* Docker Support


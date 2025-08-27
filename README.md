# NiCE Home Assignment

A simple ASP.NET Core Web API that validates input, logs activity, matches an utterance against a dictionary of tasks, and returns a suggested task. Includes unit + integration tests.

---

## Tech Stack

- .NET 8 (ASP.NET Core Web API)
- FluentValidation
- Swagger (OpenAPI)
- Logging: Console
- Testing: xUnit, FluentValidation TestHelper, Microsoft.AspNetCore.Mvc.Testing

---

## Prerequisites

- **.NET SDK 8.0+** (`dotnet --version` to check)
- IDE: Visual Studio 2022 (ASP.NET workload) or VS Code + C# Dev Kit
- Trust local HTTPS cert (first time):  
  ```bash
  dotnet dev-certs https --trust
  ```

---

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/RoeiMesi/NiCE-Home-Assignment.git
   cd NiCE-Home-Assignment
   ```

2. Restore & build:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Install packages:  
   **Web API**
   ```bash
   dotnet add NiCE_Home_Assignment package FluentValidation.AspNetCore
   dotnet add NiCE_Home_Assignment package FluentValidation.DependencyInjectionExtensions
   dotnet add NiCE_Home_Assignment package Swashbuckle.AspNetCore
   ```

   **Tests**
   ```bash
   dotnet add NiCE_Home_Assignment.Tests package xunit
   dotnet add NiCE_Home_Assignment.Tests package xunit.runner.visualstudio
   dotnet add NiCE_Home_Assignment.Tests package Microsoft.NET.Test.Sdk
   dotnet add NiCE_Home_Assignment.Tests package FluentValidation.TestHelper
   dotnet add NiCE_Home_Assignment.Tests package Microsoft.AspNetCore.Mvc.Testing
   ```
   You can also install them manually through NuGet Package Manager
---

## Run the API

```bash
dotnet run --project NiCE-Home-Assignment
```

Swagger UI will be available at:  
`https://localhost:<port>/swagger`

Endpoint:  
`POST https://localhost:<port>/suggestTask`

---

## Example Request

```json
{
  "utterance": "I need help resetting my password",
  "userId": "12345",
  "sessionId": "abcde-67890",
  "timestamp": "2025-08-21T12:00:00Z"
}
```

**Response (200 OK):**
```json
{
  "task": "ResetPasswordTask",
  "timestamp": "2025-08-21T12:01:15.1234567Z"
}
```

---

## Tests

Run all tests:
```bash
dotnet test
```

Covers:
- Unit tests for validation & controller logic
- Integration test using in-memory server (`WebApplicationFactory<Program>`)

---

## Notes

- Dictionary-based keyword matching (case-insensitive, ignores punctuation)
- Logs requests, validation failures, and decisions
- External dependency simulation retries 3 times before failing

# FruitHealth API 🍎

A modular, testable, and production-ready .NET 9 Web API project following Clean Architecture and DDD principles.

## 🔧 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Git](https://git-scm.com/)

## 🚀 Getting Started

### Clone the Repository

```bash
git clone https://github.com/ophilippov/fruithealth.git
cd fruithealth
```

### Restore, Build, and Run (.NET CLI)

```bash
dotnet run --project Source/WebApi/FruitHealth.Api/FruitHealth.Api.csproj
```

The app is hosted on: http://localhost:5116

## 🌐 APIs

### Get Healthiest Fruits

#### 📝 Description

Returns **fruits with their health scores** ordered by health score, descending.  
Fruits are filtered by specifying minimum and/or maximum sugar content using the `sugarMin` and `sugarMax` query parameters.

#### 🔗 Endpoint
```
GET http://localhost:5116/api/fruits/healthiest
```

#### 🧾 Query Parameters
- `sugarMin` - **required**, `double`. Minimum sugar content (inclusive) for filtering.
- `sugarMax` - **required**, `double`. Maximum sugar content (inclusive) for filtering.

#### 📌 Example Request

```
GET http://localhost:5116/api/fruits/healthiest?sugarMin=7&sugarMax=9
```
#### ✅ Example Response

```json
[
  {
    "name": "Dragonfruit",
    "score": 11.5
  },
  {
    "name": "Cherry",
    "score": -0.6
  },
  {
    "name": "Melon",
    "score": -4
  }
]
```


## ✅ Running Tests

```bash
dotnet test
```
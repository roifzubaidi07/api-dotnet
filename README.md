# api-pertama

Minimal API .NET + Entity Framework Core (SQL Server).

## Requirements

- .NET SDK (sesuai `TargetFramework` di `api-pertama.csproj`)
- SQL Server (LocalDB / SQL Server lokal)

## Konfigurasi Database

Connection string ada di `appsettings.json`:

- `ConnectionStrings:Default`

Contoh default:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=LatihanApi;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## Menjalankan API

Dari root project:

```bash
dotnet restore
dotnet run
```

Default URL (lihat `Properties/launchSettings.json`):

- HTTP: `http://localhost:5145`
- HTTPS: `https://localhost:7256`

## Endpoint

### Users

- `GET /users`  
  Ambil semua user (include `hobbies` dan `workExperiences`).

- `POST /users`  
  Body:

```json
{
  "name": "Budi",
  "email": "budi@example.com"
}
```

- `GET /users/{id}`  
  Ambil user by id (include `hobbies` dan `workExperiences`).

### Hobbies

- `POST /users/{userId}/hobbies`  
  Body:

```json
{
  "name": "Mancing"
}
```

### Work Experiences

- `POST /users/{userId}/works`  
  Body:

```json
{
  "company": "PT Maju Mundur",
  "position": "Software Engineer",
  "years": 2
}
```


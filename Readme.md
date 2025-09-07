# ProductsLab

Ett enkelt ASP .NET Core Web API med NuGet-paket:  
Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Tools, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer.  
API:t och SQL Server körs i varsin container och kommunicerar via ett isolerat nätverk i Docker Compose.

## Endpoints
- **GET /products** - listar produkter (seed: Coca-cola, Fanta, Pepsi)
- **POST /products** - skapar produkt, t.ex.:
  ```json
  { "name": "Sprite", "price": 12 }
Testguide
Clona GIT-repo.

Ha Docker Desktop nedladdat.

Öppna PowerShell i repo-roten (eller från Visual Studio) och kör:

docker compose up --build -d
Vänta tills två containrar är igång (productsapp + sqlserver) i Docker Desktop.

Öppna Swagger: http://localhost:8080/swagger/index.html

Testa GET /products och POST /products.

Stoppa / nollställa

docker compose down              # stoppa

docker compose down --volumes    # stoppa + rensa DB

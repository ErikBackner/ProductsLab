# ProductsLab

ASP.NET Core Web API (.NET 8) + EF Core + SQL Server i Docker Compose.  
API och DB körs i separata containrar.

## Endpoints
- **GET** '/products' - listar produkter (seed: Coca-cola, Fanta, Pepsi)
- **POST** '/products' - skapar produkt
  '''json
  { "name": "Sprite", "price": 12 }

GÖr såhär:
Installera Docker Desktop.

Klona repot.

I repo-roten kör:

bash
Kopiera kod
docker compose up --build -d
Öppna Swagger: http://localhost:8080/swagger/index.html

Stoppa / nollställ
bash
Kopiera kod
docker compose down              # stoppa
docker compose down --volumes    # stoppa + rensa DB

# Auction REST API

Internetowy system aukcyjny oparty o architekturę REST API, stworzony w technologii ASP.NET Core oraz Entity Framework Core.

Projekt umożliwia zarządzanie użytkownikami, aukcjami oraz ofertami składanymi przez użytkowników. Aplikacja posiada również prosty frontend inspirowany serwisami aukcyjnymi takimi jak Allegro.

---

## Funkcjonalności

### Użytkownicy

- Rejestracja użytkownika
- Pobieranie listy użytkowników
- Pobieranie użytkownika po identyfikatorze

### Aukcje

- Tworzenie aukcji
- Pobieranie listy aukcji
- Pobieranie szczegółów aukcji
- Aktualizacja aukcji
- Usuwanie aukcji
- Filtrowanie po kategorii
- Filtrowanie po statusie
- Sortowanie wyników
- Stronicowanie wyników

### Oferty

- Składanie ofert na aukcje
- Pobieranie historii ofert dla aukcji
- Walidacja ofert
- Aktualizacja najwyższej oferty

---

## Zastosowane technologie

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Dependency Injection
- Repository Pattern
- DTO Pattern

### Frontend

- HTML5
- CSS3
- JavaScript (Vanilla JS)
- Fetch API

---

## Struktura projektu

```text
AuctionRestApi
│
├── Controllers
│   ├── AuctionsController.cs
│   ├── BidsController.cs
│   └── UsersController.cs
│
├── DTOs
│
├── Models
│   ├── Auction.cs
│   ├── Bid.cs
│   └── User.cs
│
├── Repositories
│
├── Services
│
├── Data
│   └── AppDbContext.cs
│
└── Frontend
    ├── index.html
    ├── register.html
    ├── add-auction.html
    └── delete-auction.html
```

---

## Model danych

### User

| Pole      | Typ    |
| --------- | ------ |
| Id        | int    |
| Username  | string |
| Email     | string |
| FirstName | string |
| LastName  | string |

### Auction

| Pole              | Typ           |
| ----------------- | ------------- |
| Id                | int           |
| Title             | string        |
| Description       | string        |
| Category          | string        |
| StartingPrice     | decimal       |
| CurrentHighestBid | decimal       |
| StartDate         | DateTime      |
| EndDate           | DateTime      |
| Status            | AuctionStatus |
| OwnerId           | int           |

### Bid

| Pole      | Typ      |
| --------- | -------- |
| Id        | int      |
| AuctionId | int      |
| UserId    | int      |
| Amount    | decimal  |
| CreatedAt | DateTime |

---

## Statusy aukcji

```csharp
Active
Finished
Cancelled
```

---

## Endpointy API

### Users

```http
GET /users
GET /users/{id}
POST /users
```

### Auctions

```http
GET /auctions
GET /auctions/{id}
POST /auctions
PUT /auctions/{id}
DELETE /auctions/{id}
```

### Bids

```http
GET /auctions/{auctionId}/bids
POST /auctions/{auctionId}/bids
```

---

## Walidacja biznesowa

System uniemożliwia:

- składanie ofert na nieistniejące aukcje
- składanie ofert przez nieistniejących użytkowników
- składanie ofert na zakończone aukcje
- składanie ofert przed rozpoczęciem aukcji
- składanie ofert przez właściciela aukcji
- składanie ofert niższych od aktualnej najwyższej oferty
- ustawienie daty zakończenia wcześniejszej niż data rozpoczęcia

---

## Frontend

Frontend został przygotowany jako prosta aplikacja SPA korzystająca z REST API.

Dostępne podstrony:

### Strona główna

```text
index.html
```

Funkcje:

- przeglądanie aukcji
- filtrowanie
- sortowanie
- składanie ofert
- podgląd historii ofert

### Rejestracja

```text
register.html
```

Funkcje:

- tworzenie nowego użytkownika

### Dodawanie aukcji

```text
add-auction.html
```

Funkcje:

- wystawianie nowych aukcji

### Usuwanie aukcji

```text
delete-auction.html
```

Funkcje:

- usuwanie aukcji po identyfikatorze

---

## Uruchomienie projektu

### 1. Sklonuj repozytorium

```bash
git clone https://github.com/twoje-konto/AuctionRestApi.git
```

### 2. Przejdź do katalogu projektu

```bash
cd AuctionRestApi
```

### 3. Przywróć pakiety

```bash
dotnet restore
```

### 4. Utwórz bazę danych

```bash
dotnet ef database update
```

### 5. Uruchom aplikację

```bash
dotnet run
```

---

## Dokumentacja API

Po uruchomieniu aplikacji dokumentacja Swagger dostępna jest pod adresem:

```text
https://localhost:xxxx/swagger
```

---

## Autor

Projekt wykonany w celach edukacyjnych jako implementacja systemu aukcyjnego opartego o REST API i ASP.NET Core.
Julian Tonder
Mateusz Wielgat

# System Aukcyjny REST API

## 1. Opis projektu

System Aukcyjny REST API to aplikacja backendowa stworzona w technologii ASP.NET Core umożliwiająca zarządzanie aukcjami internetowymi. System pozwala na rejestrację użytkowników, tworzenie aukcji oraz składanie ofert na aktywne aukcje.

Projekt został wykonany w architekturze warstwowej zgodnie z zasadami REST.

---

## 2. Wykorzystane technologie

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger / OpenAPI
- Scalar API Reference
- C#

---

## 3. Architektura systemu

Aplikacja została zaprojektowana w architekturze warstwowej.

### Warstwy systemu

#### Controllers

Odpowiadają za obsługę żądań HTTP oraz komunikację z klientem.

#### Services

Zawierają logikę biznesową aplikacji.

#### Repositories

Realizują dostęp do danych zapisanych w bazie.

#### Database

Przechowuje dane użytkowników, aukcji oraz ofert.

### Schemat przepływu danych

Client → Controller → Service → Repository → Database

---

## 4. Model danych

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

### Relacje między encjami

- User (1) → (N) Auction
- User (1) → (N) Bid
- Auction (1) → (N) Bid

### Klucze obce

- Auction.OwnerId → User.Id
- Bid.UserId → User.Id
- Bid.AuctionId → Auction.Id

---

## 5. Endpointy API

### Użytkownicy

| Metoda | Endpoint        | Opis                        |
| ------ | --------------- | --------------------------- |
| GET    | /users          | Pobranie listy użytkowników |
| GET    | /users/{id}     | Pobranie użytkownika        |
| POST   | /users          | Dodanie użytkownika         |
| POST   | /users/register | Rejestracja użytkownika     |
| POST   | /users/login    | Logowanie użytkownika       |
| PUT    | /users/{id}     | Aktualizacja użytkownika    |
| DELETE | /users/{id}     | Usunięcie użytkownika       |

### Aukcje

| Metoda | Endpoint       | Opis                       |
| ------ | -------------- | -------------------------- |
| GET    | /auctions      | Pobranie listy aukcji      |
| GET    | /auctions/{id} | Pobranie szczegółów aukcji |
| POST   | /auctions      | Utworzenie aukcji          |
| PUT    | /auctions/{id} | Edycja aukcji              |
| DELETE | /auctions/{id} | Usunięcie aukcji           |

#### Obsługiwane parametry

- category
- status
- page
- pageSize
- sortBy

### Oferty

| Metoda | Endpoint                   | Opis                   |
| ------ | -------------------------- | ---------------------- |
| GET    | /auctions/{auctionId}/bids | Lista ofert dla aukcji |
| POST   | /auctions/{auctionId}/bids | Dodanie oferty         |

---

## 6. Walidacja biznesowa

System realizuje następujące reguły biznesowe:

- nazwa użytkownika musi być unikalna,
- adres e-mail musi być unikalny,
- nie można złożyć oferty niższej od aktualnie najwyższej,
- nie można składać ofert na zakończonych aukcjach,
- data zakończenia aukcji musi być późniejsza niż data rozpoczęcia.

---

## 7. Uruchomienie projektu

### Wymagania

- .NET SDK 10.0 lub nowszy
- SQLite

### Uruchomienie

Przywrócenie pakietów:

```bash
dotnet restore
```

Instalacja narzędzi Entity Framework Core (jeśli nie zostały wcześniej zainstalowane):
```bash
dotnet tool install --global dotnet-ef
```

Utworzenie bazy danych:

```bash
dotnet ef database update
```

Uruchomienie aplikacji:

```bash
dotnet run
```

Po uruchomieniu dokumentacja API dostępna jest pod adresem Swagger.

---

## 8. Testowanie API

Do testowania endpointów można wykorzystać:

- Swagger UI
- Scalar API Reference
- Postman

---

## 9. Autorzy

- Julian Tonder
- Mateusz Wielgat
- Filip Wojtan
- Radosław Kur

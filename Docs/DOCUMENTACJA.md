# Dokumentacja techniczna projektu

## 1. Nazwa projektu

System aukcji internetowych REST

## 2. Opis projektu

Projekt przedstawia prosty system aukcyjny oparty na architekturze REST. System umożliwia zarządzanie użytkownikami, wystawianie aukcji, przeglądanie aukcji oraz składanie ofert w ramach licytacji.

Aplikacja została wykonana w technologii ASP.NET Core Web API. Dane są przechowywane trwale w bazie SQLite z wykorzystaniem Entity Framework Core.

Frontend został wykonany jako prosta strona HTML/JavaScript umieszczona w folderze `wwwroot`. Interfejs użytkownika komunikuje się z backendem wyłącznie przez REST API.

## 3. Technologie

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Scalar API Reference
- HTML, CSS, JavaScript
- Git

## 4. Architektura aplikacji

Projekt wykorzystuje architekturę warstwową:

```text
Controller → Service → Repository → Database
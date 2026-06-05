# Dziekanat - System Zarządzania Uczelnią

## Autor
- Jakub Maj 

## Link do repozytorium
[GitHub Repository](LINK_DO_REPO)

## Opis projektu
Aplikacja REST API do zarządzania uczelnią, umożliwiająca obsługę studentów, prowadzących, ocen oraz autoryzację użytkowników.

## Zrealizowane funkcje

### Zarządzanie studentami
- Rejestracja nowego studenta (`POST /api/students`)
- Pobieranie listy studentów z paginacją (`GET /api/students`)
- Pobieranie szczegółów studenta (`GET /api/students/{id}`)
- Aktualizacja danych studenta (`PUT /api/students/{id}`)
- Zmiana statusu studenta — aktywny, urlop, skreślenie, absolwent (`PATCH /api/students/{id}/status`)
- Przypisanie studenta do kierunku studiów i roku akademickiego (`PATCH /api/students/{id}/assign`)

### Zarządzanie ocenami
- Dodawanie oceny studentowi (`POST /api/students/{id}/grades`)
- Pobieranie ocen studenta (`GET /api/students/{id}/grades`)
- Edycja oceny (`PUT /api/students/{studentId}/grades/{gradeId}`)

### Zarządzanie prowadzącymi
- Rejestracja nowego prowadzącego (`POST /api/lecturers`)
- Pobieranie listy prowadzących (`GET /api/lecturers`)
- Pobieranie szczegółów prowadzącego (`GET /api/lecturers/{id}`)
- Aktualizacja danych prowadzącego — tytuł, wydział, email (`PUT /api/lecturers/{id}`)
- Pobieranie listy kursów prowadzącego (`GET /api/lecturers/{id}/courses`)

### Autoryzacja i uwierzytelnianie
- Logowanie i generowanie tokenów JWT (`POST /api/auth/login`)
- Odświeżanie tokenu dostępu (`POST /api/auth/refresh`)
- Wylogowanie — unieważnienie tokenu (`POST /api/auth/revoke`)
- Pobieranie danych zalogowanego użytkownika (`GET /api/auth/me`)

### Walidacja
- Walidacja danych wejściowych za pomocą FluentValidation
- Automatyczna walidacja DTO przy tworzeniu i edycji studentów

### Baza danych
- Trwałe przechowywanie danych w bazie SQLite za pomocą Entity Framework Core
- Obsługa tożsamości użytkowników przez ASP.NET Identity

### Testy
- Testy integracyjne API z bazą InMemory (8 testów)
- Testy jednostkowe repozytorium generycznego (5 testów)

## Dane przykładowe
Aplikacja automatycznie tworzy przy starcie:

### Użytkownicy
| Email | Hasło | Rola |
|-------|-------|------|
| admin@uczelnia.pl | Admin@123! | Administrator |
| dziekanat@uczelnia.pl | Dziekanat@123! | DeanOfficeWorker |

### Role
- `Administrator` — pełny dostęp do systemu
- `DeanOfficeWorker` — pracownik dziekanatu
- `Lecturer` — wykładowca
- `Student` — student

## Uruchomienie projektu

### Wymagania
- .NET 10.0 SDK
- JetBrains Rider lub Visual Studio 2022

### Kroki

1. Sklonuj repozytorium:
```bash
git clone LINK_DO_REPO
cd dziekanat-lab2-main
```

2. Wykonaj migrację bazy danych:
```bash
dotnet ef database update --project Infrastructure --startup-project WebApi
```

3. Uruchom aplikację:
```bash
cd WebApi
dotnet run
```

4. Aplikacja dostępna pod adresem: `http://localhost:5247`

### Testowanie API
Użyj pliku `WebApi/WebApi.http` w Riderze lub narzędzia Postman.

Przykładowe logowanie:
```http
POST http://localhost:5247/api/auth/login
Content-Type: application/json

{
  "email": "admin@uczelnia.pl",
  "password": "Admin@123!"
}
```

### Uruchomienie testów
```bash
dotnet test
```

## Technologie
- **ASP.NET Core 10** — framework webowy
- **Entity Framework Core 10** — ORM
- **SQLite** — baza danych
- **ASP.NET Identity** — zarządzanie użytkownikami
- **JWT Bearer** — autoryzacja
- **FluentValidation** — walidacja
- **xUnit** — testy
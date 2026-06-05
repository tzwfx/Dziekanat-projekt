# Dziekanat - System Zarządzania Uczelnią

## Autor
- Jakub Maj 

## Link do repozytorium
[GitHub Repository](https://github.com/tzwfx/Dziekanat-projekt)

## Opis projektu
Aplikacja REST API do zarządzania uczelnią, zbudowana w architekturze modularnego monolitu (Clean Architecture). Umożliwia obsługę studentów, prowadzących, ocen, kierunków studiów oraz autoryzację użytkowników systemu dziekanatu.

## Zrealizowane funkcje


## Definicja encji, DTO, repozytoria, testy jednostkowe

- Definicja encji domenowych zgodnie z diagramem UML:
  - `BaseEntity` — klasa bazowa z kluczem `Guid`
  - `Person` (abstrakcyjna) → `Student`, `Lecturer`
  - `Grade`, `Course`, `DegreeProgram`, `AcademicYear`
- Typy wyliczeniowe: `StudentStatus`, `GradeValue`, `GradeType`, `DegreeType`, `CompletionType`, `Semester`
- Klasa rozszerzeń `GradeExtensions` z metodami `Value()`, `Parse()`, `GradeValues()`, `PolishName()`
- Definicja klas DTO w module `CoreApp`:
  - `PersonDto`, `PersonCreateDto`
  - `StudentSummaryDto`, `StudentDetailDto`, `StudentCreateDto`, `StudentUpdateDto`
  - `LecturerSummaryDto`, `LecturerDetailDto`, `LecturerCreateDto`, `LecturerUpdateDto`
- Definicja generycznego interfejsu repozytorium `IGenericRepositoryAsync<T>`
- Klasa `PagedResult<T>` — kontener na porcję danych z paginacją
- Definicja interfejsów repozytoriów konkretnych:
  - `IStudentRepository` — wyszukiwanie po roku, kierunku, zmiana statusu
  - `ILecturerRepository` — wyszukiwanie po kursie, tytule, wydziale
  - `IGradeRepository` — wyszukiwanie po studencie i kursie
- Implementacja generycznego repozytorium w pamięci (`MemoryGenericRepository`)
- Testy jednostkowe repozytorium w pamięci (`MemoryGenericRepositoryTest`):
  - Test dodawania encji
  - Test pobierania po Id
  - Test pobierania wszystkich
  - Test aktualizacji
  - Test usuwania

### Architektura, mapowanie, repozytoria

- Zdefiniowanie encji domenowych: `Student`, `Lecturer`, `Grade`, `Course`, `DegreeProgram`, `AcademicYear`
- Mapowanie encji na DTO za pomocą operatorów rzutowania (`explicit operator`)
- Implementacja generycznego repozytorium w pamięci (`MemoryGenericRepository`)
- Implementacja repozytoriów dla konkretnych encji: `MemoryStudentRepository`, `MemoryLecturerRepository`, `MemoryGradeRepository`
- Implementacja wzorca Unit of Work (`IUniversityUnitOfWork`, `MemoryUniversityUnitOfWork`)
- Implementacja serwisu studentów w pamięci (`MemoryStudentService`)
- Rejestracja klas w kontenerze DI jako singletony
- Pierwszy kontroler REST API dla studentów (`StudentsController`)

### Walidacja FluentValidation

- Instalacja i konfiguracja biblioteki `FluentValidation.AspNetCore`
- Walidator dla `StudentCreateDto` — imię, nazwisko, email, PESEL, rok studiów, kod programu
- Walidator dla `StudentUpdateDto`
- Walidator dla `GradeDto` — poprawność oceny, daty, identyfikatorów
- Moduł rejestracji walidatorów (`StudentModule`)
- Automatyczna walidacja żądań HTTP (400 Bad Request przy błędnych danych)
- Testowanie API za pomocą pliku `WebApi.http`

### Oceny, wyjątki, obsługa błędów

- Dodawanie oceny studentowi (`POST /api/students/{id}/grades`)
- Pobieranie ocen studenta (`GET /api/students/{id}/grades`)
- Edycja oceny — zmiana wartości, daty i typu (`PUT /api/students/{studentId}/grades/{gradeId}`)
- Definicja własnych wyjątków domenowych: `StudentNotFoundException`, `LecturerNotFoundException`, `CourseNotFoundException`
- Globalny handler wyjątków (`ProblemDetailsExceptionHandler`) — zwraca 404 zamiast 500
- Rejestracja handlera w pipeline middleware

### Zadanie 12 — API dla pracownika dziekanatu

#### Zarządzanie studentami
- Rejestracja nowego studenta (`POST /api/students`)
- Pobieranie listy studentów z paginacją (`GET /api/students`)
- Pobieranie szczegółów studenta (`GET /api/students/{id}`)
- Aktualizacja danych osobowych studenta (`PUT /api/students/{id}`)
- Zmiana statusu studenta — aktywny, urlop, skreślenie, absolwent (`PATCH /api/students/{id}/status`)
- Przypisanie studenta do kierunku studiów i roku akademickiego (`PATCH /api/students/{id}/assign`)

#### Zarządzanie prowadzącymi
- Rejestracja nowego prowadzącego (`POST /api/lecturers`)
- Pobieranie listy prowadzących (`GET /api/lecturers`)
- Pobieranie szczegółów prowadzącego (`GET /api/lecturers/{id}`)
- Aktualizacja danych prowadzącego — tytuł, wydział, email (`PUT /api/lecturers/{id}`)
- Pobieranie listy kursów prowadzącego (`GET /api/lecturers/{id}/courses`)

### Entity Framework Core, SQLite, Identity

- Instalacja paczek EF Core, SQLite, ASP.NET Identity
- Definicja `UniversityDbContext` dziedziczącego po `IdentityDbContext`
- Konfiguracja mapowania encji (TPH dla `Person` → `Student`/`Lecturer`)
- Implementacja repozytoriów EF: `EfGenericRepository`, `EfStudentRepository`, `EfLecturerRepository`, `EfGradeRepository`
- Implementacja `EfUniversityUnitOfWork`
- Definicja klas użytkowników systemu: `AppUser`, `AppRole`
- Interfejs `ISystemUser` z rolami `UserRole` i statusami `SystemUserStatus`
- Moduł rejestracji infrastruktury (`UniversityInfrastructureModule`)
- Wykonanie migracji i aktualizacja bazy danych SQLite
- Dane inicjalne — role: Administrator, DeanOfficeWorker, Lecturer, Student
- Dane inicjalne — użytkownicy: admin, pracownik dziekanatu

### Autoryzacja JWT

- Instalacja paczek JWT Bearer
- Konfiguracja `JwtSettings` z odczytem z `appsettings.json`
- Definicja polityk autoryzacji (`AppPolicies`): AdminOnly, DeanOfficeWorkerOnly, LecturerOnly, StudentOnly, ActiveUser
- DTO dla autoryzacji: `LoginDto`, `AuthResponseDto`, `RefreshTokenDto`, `UserDto`
- Encja `RefreshToken` z obsługą wygasania i unieważniania tokenów
- Implementacja `AuthService` z metodami:
  - Logowanie i generowanie tokenów JWT (`LoginAsync`)
  - Odświeżanie tokenu dostępu (`RefreshTokenAsync`)
  - Unieważnienie tokenu — wylogowanie (`RevokeTokenAsync`)
- Kontroler autoryzacji (`AuthController`):
  - `POST /api/auth/login` — logowanie
  - `POST /api/auth/refresh` — odświeżenie tokenu
  - `POST /api/auth/revoke` — wylogowanie
  - `GET /api/auth/me` — dane zalogowanego użytkownika
- Seeder użytkowników i ról (`IdentityDbSeeder`)
- Zabezpieczenie endpointów atrybutem `[Authorize]` z politykami

### Testy integracyjne

- Konfiguracja projektu testowego z `Microsoft.AspNetCore.Mvc.Testing`
- `UniversityAppTestFactory` — podmiana bazy SQLite na InMemory w testach
- Klasa `StudentsApiTest` z testami integracyjnymi:
  - Test statusu 401 dla nieautoryzowanego dostępu do listy studentów
  - Test tworzenia studenta (201 Created)
- Klasa `MemoryGenericRepositoryTest` z testami jednostkowymi:
  - Test dodawania encji
  - Test pobierania po Id
  - Test pobierania wszystkich
  - Test aktualizacji
  - Test usuwania
- Łącznie 8 testów — wszystkie zielone ✅

## Dane przykładowe

### Użytkownicy (tworzone automatycznie przy starcie)
| Email | Hasło | Rola |
|-------|-------|------|
| admin@uczelnia.pl | Admin@123! | Administrator |
| dziekanat@uczelnia.pl | Dziekanat@123! | DeanOfficeWorker |

### Role
| Rola | Opis |
|------|------|
| Administrator | Pełny dostęp do systemu |
| DeanOfficeWorker | Pracownik dziekanatu |
| Lecturer | Wykładowca |
| Student | Student |

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

**Logowanie:**
```http
POST http://localhost:5247/api/auth/login
Content-Type: application/json

{
  "email": "admin@uczelnia.pl",
  "password": "Admin@123!"
}
```

**Dostęp do chronionych endpointów:**
```http
GET http://localhost:5247/api/students
Authorization: Bearer TWOJ_TOKEN
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

Set-Content README.md @"
# ÍzKalauz - Backend (ASP.NET Core Web API)

Ez a projekt az **ÍzKalauz** receptkezelő és menütervező alkalmazás szerveroldali része. A backend biztosítja az adatok tárolását, a felhasználók kezelését és a receptek kiszolgálását a frontend számára.

## 🚀 Főbb funkciók
- **Felhasználókezelés:** Regisztráció és bejelentkezés (JWT alapú hitelesítés).
- **Receptek kezelése:** Böngészés, keresés, szűrés és adminisztrátori szerkesztés.
- **Kedvencek & Jegyzetek:** Felhasználóspecifikus adatok mentése.
- **Bevásárlólista & Heti menü:** Automatikus generálás és kezelés.
- **Adatbázis:** SQLite alapú tárolás Entity Framework Core segítségével.

## 🛠 Technológiai stack
- **Keretrendszer:** .NET 8 (ASP.NET Core Web API)
- **Adatbázis:** SQLite
- **ORM:** Entity Framework Core
- **Mapping:** AutoMapper
- **Biztonság:** BCrypt jelszótitkosítás, JWT tokenek

## 💻 Telepítés és futtatás
1. Klónozd a repository-t.
2. Navigálj az \`IzKalauzBackend\` mappába.
3. Állítsd be a kapcsolatot az \`appsettings.json\` fájlban (alapértelmezett: SQLite).
4. Futtasd az adatbázis migrációkat:
   \`\`\`bash
   dotnet ef database update
   \`\`\`
5. Indítsd el a szervert:
   \`\`\`bash
   dotnet run
   \`\`\`

## 📖 API Dokumentáció
A szerver futása közben a Swagger felület elérhető a következő címen:
\`http://localhost:5000/swagger\` (vagy a konfigurált porton).

---
*Készült a 2026-os záróvizsga projekthez.*
"@

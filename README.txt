# IzKalauz Backend

ASP.NET Core + SQLite project.

## Run
1. Open in VS Code or Visual Studio.
2. In terminal run:
   dotnet restore
   dotnet ef database update
   dotnet run
3. Swagger UI:
   https://localhost:5150/index.html
   or
   http://localhost:5151/index.html

User: admin@izkalauz.hu
Password: admin123

Only own recipes can be modified/deleted (JWT authentication).

Название: ProjectManagement.
Описание: REST API Для управления проектами.

Запуск: Для запуска и проверки работоспособности необходимо в файле appsettings.json изменить данные для подключение к базе данных.
База данных: PostgreSQL
Необходимые строки:

"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Port=5432;Database=ProjectsManagement;Username=postgres;Password=12345"
}

После необходимо обновить базу данных:
В терминал: dotnet ef database update

Если миграция не выполнена, то:
В терминал: dotnet ef migrations add InitialCreate

Спасибо за внимание!

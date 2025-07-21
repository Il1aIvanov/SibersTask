# SibersTask — Backend(.NET 9 + PostgreSQL)

## Описание

Это backend программы для занесения данных о проекте в БД. 
Реализовано на **.NET 9 Web API** с использованием **трёхуровневой архитектуры** (DataAccess → BusinessLogic → WebApi) и **Entity Framework Core (Code First)**.


Приложение устойчиво к ошибкам пользователя, обрабатывает исключения, валидирует входные данные и покрыто юнит-тестами.

---

##  Возможности

- CRUD-операции с проектами и сотрудниками
- Добавление/удаление сотрудников в проектах (многие-ко-многим)
- Фильтрация и сортировка проектов:
    - по диапазону дат начала
    - по приоритету
    - по подстроке в названии
    - по основным полям (`StartDate`, `Priority`, `ProjectName`)
- Swagger-интерфейс для тестирования API

---

## 🛠️ Стек технологий

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ASP.NET Core Web API
- Entity Framework Core 9
- PostgreSQL
- Swagger / Swashbuckle
- xUnit + Moq (юнит-тесты)

---

## Установка и запуск

1. Установите .NET 9 SDK
2. Установите PostgreSQL
3. В папке `WebApi/` создайте файл `.env` для подключения к БД, как описано в .env.example
4. Выполните миграции и запустите проект



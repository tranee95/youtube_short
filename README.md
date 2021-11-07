# Vitouch Back
Api для работы с приложением Vitouch

### Технологии
Что используеться впроекте:

* [ASP Net Core 3.1](https://docs.microsoft.com/ru-ru/dotnet/fundamentals/) - это кроссплатформенная реализация .NET для веб-сайтов, серверов и консольных приложений в Windows, Linux и macOS.
* [Entity Framework Core](https://docs.microsoft.com/ru-ru/ef/core/) - технологии доступа к данным Entity Framework
* [PostgreSql](https://www.postgresql.org/) - СУБД.
* [Docker](https://www.docker.com/get-started) - программное обеспечение для автоматизации развёртывания и управления приложениями в средах с поддержкой контейнеризации

### Development

Команды для начала работы с проектом.
Собираем проект.
```sh
$ cd vitouch
$ dotnet build vitouch.csproj
```
Запускаем преокт
```sh
$ dotnet run -p vitouch.csproj
```

Запуск тестов
```sh
$ cd DomainTests
$ dotnet test
```

### Docker
Работа с докером
Dillinger очень легко установить и развернуть в контейнере Docker.  Проект запускаеться на сервере, в контейнере и пробрасывается на 8080 порт.

Чтобы запустить приложение на сервере необходимо выполнить команныды

Делаем контейнер
```sh
cd vitouch_back
docker build -t <youruser>/vitouch .
```

Публикация контейнера
```sh
docker push <youruser>/vitouch
```

Запуск контейрена на сервре
```sh
docker run -p 8080:80 <youruser>/vitouch
```

### Описание проектов

* Common - Библиотека классов, тут храним все сущности проекта
* DataContext - Библиотека классов, проект для конфигурирования EF
* Domain -  Библиотека классов, реализация команд 
* DomainTests - Библиотека классов, тесты команд
* Service - Библиотека классов, основыне сервисы для работы 
* viTouch - Основное приложение

### Основые подходы к проекту

viTouch построен по патерну Паттерн Посредник ([CQRS/MediatR](https://docs.microsoft.com/ru-ru/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)).
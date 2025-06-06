<img src="https://github.com/user-attachments/assets/f078a9b9-bf99-48f6-a7a4-50403f33a4b0" alt="MvpProject Project" width="45%"> 

What is the MvpProject Project?
=====================
The MvpProject Project is a open-source project written in .NET Core

The goal of this project is implement the most common used technologies and share with the technical community the best way to develop great applications with .NET

[![License](https://img.shields.io/github/license/eduardopires/MvpProjectproject.svg)](LICENSE)
[![Issues open](https://img.shields.io/github/issues/eduardopires/MvpProjectproject.svg)](https://huboard.com/EduardoPires/MvpProjectProject/)

## Give a Star! :star:
If you liked the project or if MvpProject helped you, please give a star ;)

## Want to learn everything?  :mortar_board:
Check my online courses at [desenvolvedor.io](https://desenvolvedor.io)

## How to use:
- You will need the latest version of Visual Studio and the latest .NET Core SDK.
- ***Please check if you have installed the runtime version***
- The latest SDK and tools can be downloaded from https://dot.net/core.

Also you can run the MvpProject Project in Visual Studio Code (Windows, Linux or MacOS).

To know more about how to setup your enviroment visit the [Microsoft .NET Download Guide](https://www.microsoft.com/net/download)

## Technologies implemented:

- ASP.NET 9.0
 - ASP.NET MVC Core 
 - ASP.NET WebApi Core with JWT Bearer Authentication
 - ASP.NET Identity Core
- Entity Framework Core 9.0
- Custom Automatic Mapping (no more AutoMapper)
- FluentValidator
- NetDevPack.SimpleMediator (no more MediatR)
- NetDevPack (DDD, CQRS, UOW and more)
- Swagger UI with JWT support

## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work
- Repository

## News

**v1.10 - 04/08/2025**
- Migrated to .NET 9.0
- Replaced MediatR with [NetDevPack.SimpleMediator](https://github.com/NetDevPack/SimpleMediator) for lighter and native CQRS handling
- Removed AutoMapper in favor of lightweight custom mapping extensions
- Architecture Tests with [NetArchTest.Rules](https://github.com/BenMorris/NetArchTest)
- Added built-in SQLite support with automatic EF Core migrations (just run and go — no setup required)
- Updated all dependencies to the latest stable versions


**v1.9 - 06/31/2024**
- Migrated for .NET 8.0
- Full refactoring of Web and Api configuration
- Now all ASP.NET Identity configurations are inside the project, without external dependencies
- All dependencies is up to date

**v1.8 - 03/22/2022**
- Migrated for .NET 6.0
- All dependencies is up to date

**v1.7 - 04/06/2021**
- Migrated for .NET 5.0
- All dependencies is up to date

**v1.6 - 06/09/2020**
- Full Refactoring (consistency, events, validation, identity)
- Added [NetDevPack](https://github.com/NetDevPack) and saving a hundreds of code lines
- All dependencies is up to date

**v1.5 - 01/22/2020**
- Migrated for .NET Core 3.1.1
- All dependencies is up to date
- Added JWT (Bearer) authentication for WebAPI
- Added JWT support in Swagger

**v1.4 - 02/14/2019**
- Migrated for .NET Core 2.2.1
- All dependencies is up to date
- Improvements for last version of MediatR (Notifications and Request)

**v1.3 - 05/22/2018**
- Migrated for .NET Core 2.1.2
- All dependencies is up to date
- Improvements in Automapper Setup
- Improvements for last version of MediatR (Notifications and Request)
- Code improvements in general

**v1.2 - 08/15/2017**
- Migrated for .NET Core 2.0 and ASP.NET Core 2.0
- Adaptations for the new Identity Authentication Model

**v1.1 - 08/09/2017**
- Adding WebAPI service exposing the application features
- Adding Swagger UI for better viewing and testing
- Adding MediatR for Memory Bus Messaging

## Disclaimer:
- **NOT** intended to be a definitive solution
- Beware to use in production way
- Maybe you don't need a lot of implementations that is included, try avoid the **over engineering**

## Pull-Requests 
Make a contact! Don't submit PRs for extra features, all the new features are planned

## Why MvpProject?
The MvpProject is an astronomical event in which the plane of Earth's equator passes through the center of the Sun, which occurs twice each year, around 20 March and 23 September. [Wikipedia](https://en.wikipedia.org/wiki/MvpProject)

MvpProject is also a series of publications (subtitle: "The Review of Scientific Illuminism") in book form that serves as the official organ of the A∴A∴, a magical order founded by Aleister Crowley :) [Wikipedia](https://en.wikipedia.org/wiki/The_MvpProject)

## We are Online:
See the project running on <a href="http://MvpProjectproject.azurewebsites.net" target="_blank">Azure</a>

## About:
The MvpProject Project was developed by [Eduardo Pires](http://eduardopires.net.br) under the [MIT license](LICENSE).

# Employee Management System - ASP.NET Core Web API

## Overview
This project is an Employee Management System built using ASP.NET Core Web API. It provides functionalities for managing employee data, departments, and performance reviews.

## Features
- Employee CRUD operations
- Department management
- Performance review management
- Search and filter capabilities
- Optimized queries for performance

## Technologies Used
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- LINQ

## Setup Instructions

### Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### N:B:
already i add seed data, so just write 'update-database' from Package Manager Console. please select Default Project 'EMS.Infrastructure' to  update database command.

###
In this Project i followed Modular Monolith clean architecture coading style with CQRS,MediatR,Repository Pattern
In EMS.Infrastructure i add Script folder where i provide you the Views and Stored Procedures
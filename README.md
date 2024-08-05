<h1 align="center">Parking - Backend</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads"><img alt="SQL Server" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /></a>
</p>

## :computer: Project

This project simulates a small `parking` management system, this application was developed for academic purposes.

## :blue_book: Business Rule

- `Customer registration`: on the first visit, the customer is asked to register by providing personal details. 

- `Entry to Parking Lot`: to store the vehicle in the parking lot, a `ticket` is issued with the vehicle's `permanence` data, informing the customer of the `entry date`, `license plate` and `references`.

- `Ticket Delivered to Customer`: most of the fields are filled in when the stay is opened, the fields for `date of exit` and `total value` are left open, while the `stay status` field receives the value `parked` indicating that the vehicle is in the parking lot.

- `Vehicle pickup`: the customer presents the `ticket` given at the opening of the stay for the `pickup` procedure, this value is changed to the `stay status` field, at this stage the `date of departure` and `total amount` fields are recorded with their values informing the `date and time` and the `total amount` of the vehicle's stay in the parking lot.  

## :hammer: Features

`Operations`: For all entities in the application, you can perform basic operations such as `listing`, `searching all records`, `searching for individual records`, `creating`, `updating`, and `deleting`.

`Security`: New `Users` can be registered, and processes for `Authentication` and `Authorization` of these users are implemented.

## ✔️ Resources Used

- `.NET 8.0`
- `ASP.NET Core WebAPI`
- `C#`
- `MVC pattern`
- `Repository Pattern`
- `Service Pattern`
- `Layered Architecture`
- `Entity Framework Core`
- `SQL Server`
- `AutoMapper`
- `Microsoft Identity`
- `JWT`
- `Swagger`
- `XUnit`
- `Moq`
- `Docker`
- `Itext7`

## :floppy_disk: Clone Repository

```bash
git clone https://github.com/PauloAlves8039/dotnet-parking-backend.git
```

## :boy: Author

<a href="https://github.com/PauloAlves8039"><img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70></a>
[Paulo Alves](https://github.com/PauloAlves8039)

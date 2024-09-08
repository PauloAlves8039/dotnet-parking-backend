<h1 align="center">Parking - Backend</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads"><img alt="SQL Server" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /></a>
  <a href="https://www.docker.com/"><img alt="Docker" src="https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white" /></a>
</p>

## :computer: Project

This project simulates a small `parking` management system, this application was developed for academic purposes.

## :blue_book: Business Rule

- `Customer registration`: on the first visit, the customer is asked to register by providing personal details. 

- `Entry to Parking Lot`: to store the vehicle in the parking lot, a `ticket` is issued with the vehicle's `permanence` data, informing the customer of the `entry date`, `license plate` and `references`.

- `Ticket Delivered to Customer`: most of the fields are filled in when the stay is opened, the fields for `date of exit` and `total value` are left open, while the `stay status` field receives the value `parked` indicating that the vehicle is in the parking lot.

- `Vehicle pickup`: the customer presents the `ticket` given at the opening of the stay for the `pickup` procedure, this value is changed to the `stay status` field, at this stage the `date of departure` and `total amount` fields are recorded with their values informing the `date and time` and the `total amount` of the vehicle's stay in the parking lot.  

## :hammer: Features

`Operations`: for all entities in the application, you can perform basic operations such as `listing`, `searching all records`, `searching for individual records`, `creating`, `updating`, and `deleting`.

`Security`: new `Users` can be registered, and processes for `Authentication` and `Authorization` of these users are implemented.

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

## :white_check_mark: Technical Decisions

- `Creation of 5 Layers`: the aim was to modularize the project by dividing up the responsibilities of each layer.
- `Code Maintenance`: the modularized application makes it easier to add new features or make future corrections as required.
- `Unit Tests`: the organization of the application modules makes it easier to create unit tests.
- `Adding Docker`: the aim is to allow the application to be used in other environments, in particular your database implemented in a more practical way.
- `Generation of PDFs`: the creation of files in PDF format so that the records of the Stay model class can simulate a real system.   

## :floppy_disk: Clone Repository

```bash
git clone https://github.com/PauloAlves8039/dotnet-parking-backend.git
```

## :arrow_down: How to Use

### Using Visual Studio Code:

- `Creating the Database`: after cloning the repository navigate to the `Parking.WebAPI` project using the terminal and, run the command `dotnet ef database update --context ApplicationDbContext` to restore the database with yours tables.
- `Restoring the IdentityDbContext`: after cloning the repository navigate to the `Parking.WebAPI` project using the terminal and, run the command `dotnet ef database update --context IdentityApplicationDbContext` to restore the Identity tables.
- `Using Docker`: navigate to the root folder of the project and run the `docker-compose up --build` command to create all the elements related to the Docker configuration.

### Using Visual Studio:

- `Creating the database`: after cloning the repository go to `Tools` and open the `Package Manager Console` selecting the `Parking.WebAPI` project and run the `Update-Database -Context ApplicationDbContext` command to restore the database with yours tables.
- `Restore the IdentityDbContext`: navigate to the `Parking.WebAPI` project using the terminal, then run the command `Update-Database -Context IdentityApplicationDbContext` to restore the Identity tables.
- `Using Docker`: the `Visual Studio` can restore the `Docker` settings automatically, if you prefer you can perform the process mentioned above. 

### Scripts Database:

- `Updating ConnectionString`: change your ConnectionStrings in `appsettings.json` and `ApplicationDbContext` to `Server=sqlserver;Database=Parking;User=sa;Password=your_password; TrustServerCertificate=True` 

- `Creating a Containerized Database`: after restoring the images and containers related to the application in `Docker`, go to `SQL Server` and create a database `Parking`, then run the scripts 
[ApplicationDbContext.sql](https://github.com/PauloAlves8039/dotnet-parking-backend/blob/master/src/Parking.WebAPI/Resources/Scripts/ApplicationDbContext.sql) 
and [IdentityApplicationDbContext.sql](https://github.com/PauloAlves8039/dotnet-parking-backend/blob/master/src/Parking.WebAPI/Resources/Scripts/IdentityApplicationDbContext.sql) to mount all the tables.  

## :camera: Database Diagram

<p align="center"> <img src="https://github.com/PauloAlves8039/dotnet-parking-backend/blob/master/src/Parking.WebAPI/Resources/Images/screenshot1.png" /></p>

## :camera: WebAPI

<p align="center"> <img src="https://github.com/PauloAlves8039/dotnet-parking-backend/blob/master/src/Parking.WebAPI/Resources/Images/screenshot2.png" /></p>

## :boy: Author

<a href="https://github.com/PauloAlves8039"><img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70></a>
[Paulo Alves](https://github.com/PauloAlves8039)

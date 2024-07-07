using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Complement = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FederativeUnit = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Addresse__3214EC07CB7D83E4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Brand = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    VehicleYear = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vehicles__3214EC07D56A8AC5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC0794F4CDEF", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Customers__Addre__3A81B327",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC0773697324", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CustomerV__Custo__412EB0B6",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__CustomerV__Vehic__4222D4EF",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerVehicleId = table.Column<int>(type: "int", nullable: true),
                    LicensePlate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StayStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stays__3214EC078CDF02D7", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Stays__CustomerV__45F365D3",
                        column: x => x.CustomerVehicleId,
                        principalTable: "CustomerVehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__C1FF9309D04BA2C4",
                table: "Customers",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVehicles_CustomerId",
                table: "CustomerVehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVehicles_VehicleId",
                table: "CustomerVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Stays_CustomerVehicleId",
                table: "Stays",
                column: "CustomerVehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stays");

            migrationBuilder.DropTable(
                name: "CustomerVehicles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}

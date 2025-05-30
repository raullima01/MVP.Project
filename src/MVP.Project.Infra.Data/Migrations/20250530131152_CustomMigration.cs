using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVP.Project.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "varchar(100)", nullable: true),
                    StateInscription = table.Column<string>(type: "varchar(100)", nullable: true),
                    StreetAddress = table.Column<string>(type: "varchar(100)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecondaryAddress = table.Column<string>(type: "varchar(100)", nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    State = table.Column<string>(type: "varchar(100)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

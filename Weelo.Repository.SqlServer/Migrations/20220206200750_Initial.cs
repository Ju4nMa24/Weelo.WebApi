using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weelo.Repository.SqlServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "PropertyTraces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 6, 20, 7, 49, 567, DateTimeKind.Utc).AddTicks(4179),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 5, 3, 32, 12, 78, DateTimeKind.Utc).AddTicks(1484));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PropertyImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 6, 20, 7, 49, 566, DateTimeKind.Utc).AddTicks(7248));

            migrationBuilder.AlterColumn<string>(
                name: "CodeInternal",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 6, 20, 7, 49, 565, DateTimeKind.Utc).AddTicks(3777));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Owners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 6, 20, 7, 49, 412, DateTimeKind.Utc).AddTicks(2052));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Owners");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "PropertyTraces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 5, 3, 32, 12, 78, DateTimeKind.Utc).AddTicks(1484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 6, 20, 7, 49, 567, DateTimeKind.Utc).AddTicks(4179));

            migrationBuilder.AlterColumn<int>(
                name: "CodeInternal",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

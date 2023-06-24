using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterTogether.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 19, 56, 47, 392, DateTimeKind.Local).AddTicks(3210),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 652, DateTimeKind.Local).AddTicks(238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 19, 56, 47, 392, DateTimeKind.Local).AddTicks(424),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 651, DateTimeKind.Local).AddTicks(7545));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 652, DateTimeKind.Local).AddTicks(238),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 19, 56, 47, 392, DateTimeKind.Local).AddTicks(3210));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 651, DateTimeKind.Local).AddTicks(7545),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 19, 56, 47, 392, DateTimeKind.Local).AddTicks(424));
        }
    }
}

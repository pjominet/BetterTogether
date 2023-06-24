using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterTogether.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSignUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5558),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(5043));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5052),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(4567));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SignUp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(5043),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5558));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(4567),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5052));
        }
    }
}

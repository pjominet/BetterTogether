using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterTogether.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingsForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 652, DateTimeKind.Local).AddTicks(238),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 651, DateTimeKind.Local).AddTicks(7545),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(6603));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(9140),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 652, DateTimeKind.Local).AddTicks(238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(6603),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 18, 13, 1, 651, DateTimeKind.Local).AddTicks(7545));
        }
    }
}

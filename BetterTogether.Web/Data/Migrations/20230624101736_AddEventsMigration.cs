using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterTogether.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(5043));

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "SignUp",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2023, 6, 24, 12, 17, 36, 875, DateTimeKind.Local).AddTicks(4567))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignUp_EventId",
                table: "SignUp",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignUp_Event_EventId",
                table: "SignUp",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignUp_Event_EventId",
                table: "SignUp");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropIndex(
                name: "IX_SignUp_EventId",
                table: "SignUp");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SignUp");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "SignUp");
        }
    }
}

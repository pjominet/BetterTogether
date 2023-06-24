using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterTogether.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProperSignUpEventRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignUp_Event_EventId",
                table: "SignUp");

            migrationBuilder.DropIndex(
                name: "IX_SignUp_EventId",
                table: "SignUp");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "SignUp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(9140),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5558));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(6603),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.CreateTable(
                name: "EventSignUp",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    SignUpId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSignUp", x => new { x.EventId, x.SignUpId });
                    table.ForeignKey(
                        name: "FK_EventSignUp_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSignUp_SignUp_SignUpId",
                        column: x => x.SignUpId,
                        principalTable: "SignUp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSignUp_SignUpId",
                table: "EventSignUp",
                column: "SignUpId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSignUp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SignUp",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5558),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "SignUp",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 24, 15, 20, 36, 626, DateTimeKind.Local).AddTicks(5052),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 6, 24, 17, 48, 19, 194, DateTimeKind.Local).AddTicks(6603));

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
    }
}

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleBot.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagesTelegram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IEnumerable<long>>(
                name: "Enemies",
                table: "BattleSessions",
                type: "bigint[]",
                nullable: true,
                oldClrType: typeof(IEnumerable<long>),
                oldType: "bigint[]");

            migrationBuilder.AlterColumn<IEnumerable<string>>(
                name: "BattleLog",
                table: "BattleSessions",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(IEnumerable<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<IEnumerable<long>>(
                name: "Allies",
                table: "BattleSessions",
                type: "bigint[]",
                nullable: true,
                oldClrType: typeof(IEnumerable<long>),
                oldType: "bigint[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IEnumerable<long>>(
                name: "Enemies",
                table: "BattleSessions",
                type: "bigint[]",
                nullable: false,
                oldClrType: typeof(IEnumerable<long>),
                oldType: "bigint[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<IEnumerable<string>>(
                name: "BattleLog",
                table: "BattleSessions",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(IEnumerable<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<IEnumerable<long>>(
                name: "Allies",
                table: "BattleSessions",
                type: "bigint[]",
                nullable: false,
                oldClrType: typeof(IEnumerable<long>),
                oldType: "bigint[]",
                oldNullable: true);
        }
    }
}

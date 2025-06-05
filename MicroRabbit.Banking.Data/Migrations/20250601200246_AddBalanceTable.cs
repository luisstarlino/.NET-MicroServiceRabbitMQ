using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroRabbit.Banking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBalanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_balances_Accounts_AccountId",
                table: "balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_balances",
                table: "balances");

            migrationBuilder.RenameTable(
                name: "balances",
                newName: "Balances");

            migrationBuilder.RenameIndex(
                name: "IX_balances_AccountId",
                table: "Balances",
                newName: "IX_Balances_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Balances",
                table: "Balances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Accounts_AccountId",
                table: "Balances",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Accounts_AccountId",
                table: "Balances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Balances",
                table: "Balances");

            migrationBuilder.RenameTable(
                name: "Balances",
                newName: "balances");

            migrationBuilder.RenameIndex(
                name: "IX_Balances_AccountId",
                table: "balances",
                newName: "IX_balances_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_balances",
                table: "balances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_balances_Accounts_AccountId",
                table: "balances",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroRabbit.Analytics.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "ClientApprovals",
                newName: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ClientApprovals",
                newName: "AccountId");
        }
    }
}

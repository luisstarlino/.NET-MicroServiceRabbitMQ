using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroRabbit.Banking.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassWord",
                table: "Clients",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clients",
                newName: "Mail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Clients",
                newName: "PassWord");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Clients",
                newName: "Email");
        }
    }
}

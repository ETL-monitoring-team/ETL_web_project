using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_web_project.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Email",
                schema: "auth",
                table: "UserAccount",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Username",
                schema: "auth",
                table: "UserAccount",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAccount_Email",
                schema: "auth",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_Username",
                schema: "auth",
                table: "UserAccount");
        }
    }
}

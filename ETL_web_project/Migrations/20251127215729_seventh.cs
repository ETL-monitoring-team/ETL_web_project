using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_web_project.Migrations
{
    /// <inheritdoc />
    public partial class seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                schema: "auth",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                schema: "auth",
                table: "UserAccount",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                schema: "auth",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                schema: "auth",
                table: "UserAccount");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_web_project.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EtlSchedule",
                schema: "etl",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    FrequencyText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlSchedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_EtlSchedule_EtlJob_JobId",
                        column: x => x.JobId,
                        principalSchema: "etl",
                        principalTable: "EtlJob",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EtlSchedule_JobId",
                schema: "etl",
                table: "EtlSchedule",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtlSchedule",
                schema: "etl");
        }
    }
}

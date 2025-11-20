using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_web_project.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dw");

            migrationBuilder.EnsureSchema(
                name: "etl");

            migrationBuilder.EnsureSchema(
                name: "stg");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "DimCustomer",
                schema: "dw",
                columns: table => new
                {
                    CustomerKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimCustomer", x => x.CustomerKey);
                });

            migrationBuilder.CreateTable(
                name: "DimDate",
                schema: "dw",
                columns: table => new
                {
                    DateKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Day = table.Column<byte>(type: "tinyint", nullable: false),
                    MonthName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DayOfWeek = table.Column<byte>(type: "tinyint", nullable: true),
                    DayName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimDate", x => x.DateKey);
                });

            migrationBuilder.CreateTable(
                name: "DimProduct",
                schema: "dw",
                columns: table => new
                {
                    ProductKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimProduct", x => x.ProductKey);
                });

            migrationBuilder.CreateTable(
                name: "DimStore",
                schema: "dw",
                columns: table => new
                {
                    StoreKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimStore", x => x.StoreKey);
                });

            migrationBuilder.CreateTable(
                name: "EtlJob",
                schema: "etl",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlJob", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "SalesRaw",
                schema: "stg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRaw", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FactSales",
                schema: "dw",
                columns: table => new
                {
                    SalesKey = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateKey = table.Column<int>(type: "int", nullable: false),
                    StoreKey = table.Column<int>(type: "int", nullable: false),
                    ProductKey = table.Column<int>(type: "int", nullable: false),
                    CustomerKey = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactSales", x => x.SalesKey);
                    table.ForeignKey(
                        name: "FK_FactSales_DimCustomer_CustomerKey",
                        column: x => x.CustomerKey,
                        principalSchema: "dw",
                        principalTable: "DimCustomer",
                        principalColumn: "CustomerKey");
                    table.ForeignKey(
                        name: "FK_FactSales_DimDate_DateKey",
                        column: x => x.DateKey,
                        principalSchema: "dw",
                        principalTable: "DimDate",
                        principalColumn: "DateKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactSales_DimProduct_ProductKey",
                        column: x => x.ProductKey,
                        principalSchema: "dw",
                        principalTable: "DimProduct",
                        principalColumn: "ProductKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactSales_DimStore_StoreKey",
                        column: x => x.StoreKey,
                        principalSchema: "dw",
                        principalTable: "DimStore",
                        principalColumn: "StoreKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtlRun",
                schema: "etl",
                columns: table => new
                {
                    RunId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowsRead = table.Column<int>(type: "int", nullable: true),
                    RowsInserted = table.Column<int>(type: "int", nullable: true),
                    RowsUpdated = table.Column<int>(type: "int", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlRun", x => x.RunId);
                    table.ForeignKey(
                        name: "FK_EtlRun_EtlJob_JobId",
                        column: x => x.JobId,
                        principalSchema: "etl",
                        principalTable: "EtlJob",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtlLog",
                schema: "etl",
                columns: table => new
                {
                    LogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunId = table.Column<long>(type: "bigint", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtlLog", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_EtlLog_EtlRun_RunId",
                        column: x => x.RunId,
                        principalSchema: "etl",
                        principalTable: "EtlRun",
                        principalColumn: "RunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EtlLog_RunId",
                schema: "etl",
                table: "EtlLog",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_EtlRun_JobId",
                schema: "etl",
                table: "EtlRun",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_FactSales_CustomerKey",
                schema: "dw",
                table: "FactSales",
                column: "CustomerKey");

            migrationBuilder.CreateIndex(
                name: "IX_FactSales_DateKey",
                schema: "dw",
                table: "FactSales",
                column: "DateKey");

            migrationBuilder.CreateIndex(
                name: "IX_FactSales_ProductKey",
                schema: "dw",
                table: "FactSales",
                column: "ProductKey");

            migrationBuilder.CreateIndex(
                name: "IX_FactSales_StoreKey",
                schema: "dw",
                table: "FactSales",
                column: "StoreKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtlLog",
                schema: "etl");

            migrationBuilder.DropTable(
                name: "FactSales",
                schema: "dw");

            migrationBuilder.DropTable(
                name: "SalesRaw",
                schema: "stg");

            migrationBuilder.DropTable(
                name: "UserAccount",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "EtlRun",
                schema: "etl");

            migrationBuilder.DropTable(
                name: "DimCustomer",
                schema: "dw");

            migrationBuilder.DropTable(
                name: "DimDate",
                schema: "dw");

            migrationBuilder.DropTable(
                name: "DimProduct",
                schema: "dw");

            migrationBuilder.DropTable(
                name: "DimStore",
                schema: "dw");

            migrationBuilder.DropTable(
                name: "EtlJob",
                schema: "etl");
        }
    }
}

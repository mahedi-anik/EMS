using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMS.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Employees table
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            // Create Departments table
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create PerformanceReviews table
            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReviewNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            // Insert seed data for Employees
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "CreateBy", "CreateDate", "DepartmentId", "Email", "EmployeeName", "IsActive", "IsDelete", "JoiningDate", "Phone", "Position", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "022d2414-6306-4385-8034-280067946c52", null, null, new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8342), null, null, "Jane", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Developer", "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8341) },
                    { "6f368bcb-8c5f-4db7-a857-1342189475ee", null, null, new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8338), null, null, "John", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Manager", "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8335) }
                });

            // Insert seed data for Departments
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Budget", "CreateBy", "CreateDate", "DepartmentName", "IsActive", "IsDelete", "ManagerId", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "250a8d45-f79c-47ab-8c54-f64428d30578", 50000m, "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8178), "HR", true, false, "6f368bcb-8c5f-4db7-a857-1342189475ee", "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8172) },
                    { "82c23b26-46f0-4e44-8936-39b247a845ab", 100000m, "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8185), "IT", true, false, "6f368bcb-8c5f-4db7-a857-1342189475ee", "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8183) }
                });

            // Insert seed data for PerformanceReviews
            migrationBuilder.InsertData(
                table: "PerformanceReviews",
                columns: new[] { "Id", "CreateBy", "CreateDate", "EmployeeId", "IsActive", "IsDelete", "ReviewDate", "ReviewNote", "ReviewScore", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "9d6610bd-b3f2-423a-b62f-fdba5259f320", "admin", new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8393), "022d2414-6306-4385-8034-280067946c52", true, false, new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8391), "Good performance", 3.8m, null, new DateTime(2024, 12, 18, 12, 10, 55, 271, DateTimeKind.Utc).AddTicks(8390) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

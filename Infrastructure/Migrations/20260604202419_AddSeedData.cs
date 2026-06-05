using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "fca2f2f4-dc34-422a-bd34-28e866b909a6", null, "Administrator", "ADMINISTRATOR" },
                    { "2", "a40a5edc-ca06-43a8-bea6-1d62e416a1a6", null, "DeanOfficeWorker", "DEANofficeworker" },
                    { "3", "9c58f228-aeb2-4a78-be26-426e02f4f56e", null, "Lecturer", "LECTURER" },
                    { "4", "ed1c7e85-35fc-424c-ba36-716d2f262d5f", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeactivatedAt", "Department", "Email", "EmailConfirmed", "FirstName", "FullName", "LastLoginAt", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-1", 0, "b4577597-b349-4aaa-bfdb-c6f6b3bb4185", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "IT", "admin@uczelnia.pl", true, "Admin", "Admin System", null, "System", false, null, "ADMIN@UCZELNIA.PL", "ADMIN@UCZELNIA.PL", "AQAAAAIAAYagAAAAEH+dummy+hash+for+seed+data", null, false, "ecb6e322-d3cb-4847-bc96-a703ac7b4e80", 0, false, "admin@uczelnia.pl" },
                    { "user-2", 0, "85caaf81-ace4-4c76-ae49-2eb3ed30d854", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dziekanat", "dziekanat@uczelnia.pl", true, "Anna", "Anna Kowalska", null, "Kowalska", false, null, "DZIEKANAT@UCZELNIA.PL", "DZIEKANAT@UCZELNIA.PL", "AQAAAAIAAYagAAAAEH+dummy+hash+for+seed+data", null, false, "4a1c442a-38d9-40ab-825c-69302092fac8", 0, false, "dziekanat@uczelnia.pl" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-2");
        }
    }
}

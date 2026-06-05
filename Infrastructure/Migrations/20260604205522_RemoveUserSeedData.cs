using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "74f753be-b09a-45af-af1b-7923d4633033");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "eb3a857a-ca16-44b8-92fa-611b05631369", "DEANOFFICEWOKER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "bbc5316e-f06c-4115-9230-0c77ae5eb6e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "7e53cc5d-d2fd-4560-8e73-4cb2a6f7dde1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "02635b21-2d8a-493f-a05f-13668d0b78b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "1cf01bfd-5ab8-4a29-9aef-10c76bfec66f", "DEANofficeworker" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "65f461b7-4b2e-442b-b789-f13535e9bb59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9a09219a-dbe2-4c11-a840-eece3a696612");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeactivatedAt", "Department", "Email", "EmailConfirmed", "FirstName", "FullName", "LastLoginAt", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-1", 0, "61ef29a0-c9e0-4597-bbe6-8cd2ddae0948", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "IT", "admin@uczelnia.pl", true, "Admin", "Admin System", null, "System", false, null, "ADMIN@UCZELNIA.PL", "ADMIN@UCZELNIA.PL", "AQAAAAIAAYagAAAAEH+dummy+hash+for+seed+data", null, false, "a34441fc-fe42-434d-971a-c75e79c63370", 0, false, "admin@uczelnia.pl" },
                    { "user-2", 0, "6983a704-786e-4ba9-af78-67ec7b304489", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dziekanat", "dziekanat@uczelnia.pl", true, "Anna", "Anna Kowalska", null, "Kowalska", false, null, "DZIEKANAT@UCZELNIA.PL", "DZIEKANAT@UCZELNIA.PL", "AQAAAAIAAYagAAAAEH+dummy+hash+for+seed+data", null, false, "6c84b54d-63cb-4775-9b95-9315df346757", 0, false, "dziekanat@uczelnia.pl" }
                });
        }
    }
}

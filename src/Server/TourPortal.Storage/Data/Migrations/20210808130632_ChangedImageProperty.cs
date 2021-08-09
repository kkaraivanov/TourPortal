using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class ChangedImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02617fe5-1595-474f-a0d1-83c36a5be0e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "695635df-d8db-4a98-9848-030c23736f0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b52263d-5f43-4d86-96e8-1ce6a7ef6d2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aad1d92d-0c80-4350-825d-518ce26e3794");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "63f8ebab-835f-4897-83f4-3ba27fa7957a");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "b2323499-90c1-4fb1-a5ea-a03146989d56");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "bd5ca0f7-ee40-4a08-ba94-e02a6841a4d2");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "d4ab6506-a089-4453-918a-c788974dae3c");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilaImage",
                table: "UserProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f9ac6e6-9bcc-4598-8077-78aa32ac6ba6", "4316be71-790f-4fa1-bd07-67a427fe4765", "Administrator", "ADMINISTRATOR" },
                    { "102bed6f-d08f-4e36-a662-51f57074eb25", "27f1be7c-9974-425b-9b4a-1b1a21768cda", "User", "USER" },
                    { "9d94359e-7275-41b7-a78b-2e834a5ffd9b", "df3a4f1d-e146-4eaf-b8b1-daacca30e234", "Owner", "OWNER" },
                    { "34fd5993-6a79-487a-8cfa-055190681b23", "a1b00e00-289d-403b-a306-5f234d545ce7", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "e35e3f1b-8083-4bb0-9949-630e52dbf3b1", "Single" },
                    { "da21c4f0-3286-4490-bcb7-e97bdf261603", "Double" },
                    { "805f9964-2eea-40c0-a32c-3bee1dc3f932", "Double with 2 single beds" },
                    { "b5bc36bc-524d-421b-9566-a4115dca22f8", "Apartment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "102bed6f-d08f-4e36-a662-51f57074eb25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f9ac6e6-9bcc-4598-8077-78aa32ac6ba6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34fd5993-6a79-487a-8cfa-055190681b23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d94359e-7275-41b7-a78b-2e834a5ffd9b");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "805f9964-2eea-40c0-a32c-3bee1dc3f932");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "b5bc36bc-524d-421b-9566-a4115dca22f8");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "da21c4f0-3286-4490-bcb7-e97bdf261603");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "e35e3f1b-8083-4bb0-9949-630e52dbf3b1");

            migrationBuilder.DropColumn(
                name: "ProfilaImage",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aad1d92d-0c80-4350-825d-518ce26e3794", "ec8cb832-fcbc-49b7-9c8e-285c111ddd49", "Administrator", "ADMINISTRATOR" },
                    { "695635df-d8db-4a98-9848-030c23736f0a", "173a9397-1e18-447e-92fb-91deed7515bf", "User", "USER" },
                    { "02617fe5-1595-474f-a0d1-83c36a5be0e3", "944931c8-b497-4717-bcd3-69d7d4887cb6", "Owner", "OWNER" },
                    { "9b52263d-5f43-4d86-96e8-1ce6a7ef6d2e", "750ae451-b245-4783-b541-b9de2c34d4f0", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "bd5ca0f7-ee40-4a08-ba94-e02a6841a4d2", "Single" },
                    { "d4ab6506-a089-4453-918a-c788974dae3c", "Double" },
                    { "63f8ebab-835f-4897-83f4-3ba27fa7957a", "Double with 2 single beds" },
                    { "b2323499-90c1-4fb1-a5ea-a03146989d56", "Apartment" }
                });
        }
    }
}

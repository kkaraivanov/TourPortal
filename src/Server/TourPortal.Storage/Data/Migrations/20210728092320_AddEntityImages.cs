using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class AddEntityImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomImages_Rooms_RoomId",
                table: "RoomImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomImages",
                table: "RoomImages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "117df84f-168b-4de4-a4e3-71f92dec3eda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58977f6a-e449-4a8a-8ad5-bdc978f999bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bed1740a-1858-4d69-ab50-891d01ff3967");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa797f56-0646-4b0a-8404-3bd576db56aa");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "148232ac-223a-4c04-93f9-ac66fdbe9698");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "82dd054a-e073-4492-b709-d47bc44a1cbd");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "8a78fd6e-5675-40b0-b940-38fb071e6e54");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "8ab2f733-343e-4348-b52e-39897959a982");

            migrationBuilder.RenameTable(
                name: "RoomImages",
                newName: "RoomImageses");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImageses",
                newName: "IX_RoomImageses_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomImageses",
                table: "RoomImageses",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImageses_Rooms_RoomId",
                table: "RoomImageses",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomImageses_Rooms_RoomId",
                table: "RoomImageses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomImageses",
                table: "RoomImageses");

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

            migrationBuilder.RenameTable(
                name: "RoomImageses",
                newName: "RoomImages");

            migrationBuilder.RenameIndex(
                name: "IX_RoomImageses_RoomId",
                table: "RoomImages",
                newName: "IX_RoomImages_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomImages",
                table: "RoomImages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bed1740a-1858-4d69-ab50-891d01ff3967", "da225f62-60ff-4ca0-849e-24834aba6942", "Administrator", "ADMINISTRATOR" },
                    { "58977f6a-e449-4a8a-8ad5-bdc978f999bf", "d8c68ca2-f8af-442b-87e6-2a2e42c77664", "User", "USER" },
                    { "117df84f-168b-4de4-a4e3-71f92dec3eda", "65231424-fe63-42da-86dc-fb4bb1dfa919", "Owner", "OWNER" },
                    { "fa797f56-0646-4b0a-8404-3bd576db56aa", "c59fe5f9-87b1-4091-9290-6c42490775da", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "8a78fd6e-5675-40b0-b940-38fb071e6e54", "Single" },
                    { "148232ac-223a-4c04-93f9-ac66fdbe9698", "Double" },
                    { "82dd054a-e073-4492-b709-d47bc44a1cbd", "Double with 2 single beds" },
                    { "8ab2f733-343e-4348-b52e-39897959a982", "Apartment" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImages_Rooms_RoomId",
                table: "RoomImages",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

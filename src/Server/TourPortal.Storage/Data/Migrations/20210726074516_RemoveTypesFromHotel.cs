using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class RemoveTypesFromHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_Hotels_HotelId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_HotelId",
                table: "RoomTypes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f5f6f54-6051-47bb-9a8d-f3a17d3f582b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afa60348-2877-4c51-ab7d-a3b04318a820");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2a8987e-3fe6-47cc-8289-2e6405bfa1c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9b34a1d-f3b8-4511-a6e5-7684d484bdce");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "83e38a63-6db6-4d0a-8321-67bb4b7a3540");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "92244421-22ba-4c92-87f0-22bfc13cb30d");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "93e4d726-552d-43cb-914a-795d88525dd4");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "ba809515-70a3-4886-ad29-a6cf2819b428");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "RoomTypes");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "HotelId",
                table: "RoomTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "afa60348-2877-4c51-ab7d-a3b04318a820", "3bd89fc6-a94a-4cdb-b30b-65fed573e02e", "Administrator", "ADMINISTRATOR" },
                    { "7f5f6f54-6051-47bb-9a8d-f3a17d3f582b", "9a6b60ae-9716-41b0-a9e5-bc6002a11da4", "User", "USER" },
                    { "d2a8987e-3fe6-47cc-8289-2e6405bfa1c0", "aec7b79a-2fd3-4ff3-9a2a-c4d003a4a83e", "Owner", "OWNER" },
                    { "f9b34a1d-f3b8-4511-a6e5-7684d484bdce", "7ede0299-9f57-4f0d-94c7-d49d566e4769", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "HotelId", "Type" },
                values: new object[,]
                {
                    { "92244421-22ba-4c92-87f0-22bfc13cb30d", null, "Single" },
                    { "93e4d726-552d-43cb-914a-795d88525dd4", null, "Double" },
                    { "ba809515-70a3-4886-ad29-a6cf2819b428", null, "Double with 2 single beds" },
                    { "83e38a63-6db6-4d0a-8321-67bb4b7a3540", null, "Apartment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_HotelId",
                table: "RoomTypes",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_Hotels_HotelId",
                table: "RoomTypes",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

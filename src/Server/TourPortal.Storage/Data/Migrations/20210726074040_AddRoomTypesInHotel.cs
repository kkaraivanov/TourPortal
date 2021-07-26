using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class AddRoomTypesInHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bc2a465-3405-47af-9298-d6f3aa761f70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d9c5470-4496-4d6e-b4eb-a2207a73e568");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49adb263-6366-4552-a8de-ad9f9ea3fca3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7819d43a-2c95-4402-99d8-5109561e5abf");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "5182d89b-ec5a-4e02-90dd-a0ef2c160da0");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "8812514e-1a9e-4a7f-9bb2-1641b91aaead");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "ba2e5463-b149-49a2-916b-512179600e67");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "e8fb0793-3845-4c8d-8f21-d7638a343d7a");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "2d9c5470-4496-4d6e-b4eb-a2207a73e568", "e1b23203-6bfa-404d-87cb-2413e5f16774", "Administrator", "ADMINISTRATOR" },
                    { "49adb263-6366-4552-a8de-ad9f9ea3fca3", "6c2e7fc4-46c3-4f65-a457-f72472d1c9cd", "User", "USER" },
                    { "0bc2a465-3405-47af-9298-d6f3aa761f70", "9c5c6fd1-e2c3-4838-8031-bb216418841e", "Owner", "OWNER" },
                    { "7819d43a-2c95-4402-99d8-5109561e5abf", "badd4f5e-ee81-43c6-8b5d-44e842bf1e0f", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "8812514e-1a9e-4a7f-9bb2-1641b91aaead", "Single" },
                    { "5182d89b-ec5a-4e02-90dd-a0ef2c160da0", "Double" },
                    { "ba2e5463-b149-49a2-916b-512179600e67", "Double with 2 single beds" },
                    { "e8fb0793-3845-4c8d-8f21-d7638a343d7a", "Apartment" }
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class ChangedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19fa68b2-2a52-495f-ae77-23e50f0a7698");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24f2355a-47ea-403b-9c1e-481508d4b4fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c72f349-02c8-4b2a-9587-89364c6cace6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b87cd92f-b2c9-4ff8-9594-c31cc848b8f4");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "2e5ee6da-7803-4ec0-a796-ba66a52a3bd4");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "3d671f15-df32-4763-ae70-9b4c58b54418");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "91733f71-b092-47b5-8e53-8d1cc58af5b4");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "ad64cade-c471-46ce-81e5-d81392e481b7");

            migrationBuilder.RenameColumn(
                name: "Sity",
                table: "Hotels",
                newName: "City");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Hotels",
                newName: "Sity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b87cd92f-b2c9-4ff8-9594-c31cc848b8f4", "f11a6837-293a-473a-90bb-fba6c6fb1d1d", "Administrator", "ADMINISTRATOR" },
                    { "4c72f349-02c8-4b2a-9587-89364c6cace6", "d79260da-37eb-447b-945d-216bcceb790b", "User", "USER" },
                    { "24f2355a-47ea-403b-9c1e-481508d4b4fe", "3e537a2f-4c64-4250-8058-8a158da7d5db", "Owner", "OWNER" },
                    { "19fa68b2-2a52-495f-ae77-23e50f0a7698", "85dd79a5-1d64-475c-9a52-e09b6e1848b5", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "2e5ee6da-7803-4ec0-a796-ba66a52a3bd4", "Single" },
                    { "91733f71-b092-47b5-8e53-8d1cc58af5b4", "Double" },
                    { "ad64cade-c471-46ce-81e5-d81392e481b7", "Double with 2 single beds" },
                    { "3d671f15-df32-4763-ae70-9b4c58b54418", "Apartment" }
                });
        }
    }
}

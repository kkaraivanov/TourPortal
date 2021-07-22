using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class AddPropertiesToHotelModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "093bb895-83e0-4944-a1d9-548f7d48ff2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "497568cf-d146-4dbb-8ac0-692ebd7c1815");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65df8aff-c2b9-4f27-9b98-cee6ce630b69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a43e17e-43e4-4744-a320-e6f57518927e");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "0d64c8ae-40d6-4ea2-ae2a-fd9618fa3aeb");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "944ea5d6-e7b7-4bf4-a59f-24c173192229");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "bb7bb90d-ccd4-42f9-b86e-f3a35d16e20d");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "d29efb5d-6eb4-4522-ae04-956e4168deb6");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPersons",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "757f3c82-8a6c-40aa-bb81-af9d40dc2366", "95e197e5-893b-4634-9a8c-6c8be0515cf5", "Administrator", "ADMINISTRATOR" },
                    { "2e04eef7-d152-4539-be8a-983c11e10437", "974db922-71ca-4c1a-ab65-2946cbbaf60d", "User", "USER" },
                    { "9ee39629-c192-4683-a157-bf8ef54ce60d", "e232f0f5-6e6a-4b2e-b146-a7fedfea48b7", "Owner", "OWNER" },
                    { "1dfa1005-369e-4e22-984a-2c720240b568", "0273b932-43e6-48c6-a8dd-1f7d118e20af", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "c6722019-2b43-4813-8822-6d9eaa4c4a1b", "Single" },
                    { "59a45f05-bf80-4520-adfa-7ab8806609cc", "Double" },
                    { "b97b98d5-7403-4c52-bf8f-b661e2650f72", "Double with 2 single beds" },
                    { "d114fc3e-6d5b-4d25-b27b-e9732c69c31e", "Apartment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dfa1005-369e-4e22-984a-2c720240b568");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e04eef7-d152-4539-be8a-983c11e10437");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "757f3c82-8a6c-40aa-bb81-af9d40dc2366");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ee39629-c192-4683-a157-bf8ef54ce60d");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "59a45f05-bf80-4520-adfa-7ab8806609cc");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "b97b98d5-7403-4c52-bf8f-b661e2650f72");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "c6722019-2b43-4813-8822-6d9eaa4c4a1b");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: "d114fc3e-6d5b-4d25-b27b-e9732c69c31e");

            migrationBuilder.DropColumn(
                name: "NumberOfPersons",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "Hotels");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "093bb895-83e0-4944-a1d9-548f7d48ff2b", "64e2cd95-26c8-4590-863c-4578fc0569e0", "Administrator", "ADMINISTRATOR" },
                    { "497568cf-d146-4dbb-8ac0-692ebd7c1815", "6b18bee5-cd97-4a41-9c2c-0b1ddaa7803d", "User", "USER" },
                    { "9a43e17e-43e4-4744-a320-e6f57518927e", "b198a2db-ccfb-43f1-9bf0-ea334faa3c90", "Owner", "OWNER" },
                    { "65df8aff-c2b9-4f27-9b98-cee6ce630b69", "892f37c0-deeb-485f-920a-d8885305b924", "Employe", "EMPLOYE" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { "d29efb5d-6eb4-4522-ae04-956e4168deb6", "Single" },
                    { "bb7bb90d-ccd4-42f9-b86e-f3a35d16e20d", "Double" },
                    { "0d64c8ae-40d6-4ea2-ae2a-fd9618fa3aeb", "Double with 2 single beds" },
                    { "944ea5d6-e7b7-4bf4-a59f-24c173192229", "Apartment" }
                });
        }
    }
}

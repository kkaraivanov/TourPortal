using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourPortal.Storage.Data.Migrations
{
    public partial class AddManyProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "NumberOfBeds",
                table: "Rooms",
                newName: "RoomNumber");

            migrationBuilder.AddColumn<int>(
                name: "CountOfBeds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountOfPersons",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Sity",
                table: "Hotels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HotelName",
                table: "Hotels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Hotels",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelImageUrl",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_HotelId",
                table: "Contacts",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImages",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "RoomImages");

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

            migrationBuilder.DropColumn(
                name: "CountOfBeds",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CountOfPersons",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HotelImageUrl",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "Rooms",
                newName: "NumberOfBeds");

            migrationBuilder.AlterColumn<string>(
                name: "Sity",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "HotelName",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

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
    }
}

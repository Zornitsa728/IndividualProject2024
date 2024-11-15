using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e481150-4623-41cc-b510-e01e52e9f6fb", "af747ed2-7ffd-43f2-abc1-f4518b7e4cc8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c3299e5a-e86d-432c-bd07-b2faea7388d3", "363b3120-eb47-4bdd-8b68-dff9282db6c5" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/Category/default.jpg");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 15, 11, 54, 42, 60, DateTimeKind.Local).AddTicks(5872), "/images/Recipe/Sweet-Potato-Soup.jpg" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 15, 11, 54, 42, 60, DateTimeKind.Local).AddTicks(5831), "/images/Recipe/Cranberry-Pie.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c38796cb-ffe5-4478-99de-52b7db19a780", "450fdc4f-7926-4120-a1d4-230342e511d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76104c50-0372-45e5-a16b-19c36f549577", "476ee64c-c73a-4b88-a3ed-091cf89047f0" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 15, 11, 26, 5, 241, DateTimeKind.Local).AddTicks(3034), "/images/Sweet-Potato-Soup.jpg" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedOn", "ImageUrl" },
                values: new object[] { new DateTime(2024, 11, 15, 11, 26, 5, 241, DateTimeKind.Local).AddTicks(2991), "/images/Cranberry-Pie.jpg" });
        }
    }
}

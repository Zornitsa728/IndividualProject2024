using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCategoryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e1ab04ec-7f0f-4c81-bb1f-c9c13410fa54", "89dbff09-5ab7-44d5-a7e6-73884f07e950" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "df43a30a-0f4a-4a54-b555-d0853492c644", "63b31c05-2df4-4ac8-9a3c-e434d2f46034" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/Category/appetizers.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/Category/breadRecipes.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/Category/breakfast.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/Category/desserts.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/Category/drinks.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/Category/mainDishes.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/images/Category/salads.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/images/Category/sandwiches.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/Category/sideDishes.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/Category/soups.jpg");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 10, 13, 44, 44, 329, DateTimeKind.Local).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 10, 13, 44, 44, 329, DateTimeKind.Local).AddTicks(5932));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f37bee47-e31e-4813-9279-4ab58314a3f5", "b425446c-0b3c-4d99-8135-b299eca00834" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "68327269-3e15-40ff-9ce1-b46b657f90b3", "3553f169-6c10-4b5b-a81b-080f6acd4774" });

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
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 10, 13, 31, 27, 335, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 12, 10, 13, 31, 27, 335, DateTimeKind.Local).AddTicks(2567));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRecipeCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "CreatedOn",
                value: new DateTime(2024, 11, 15, 11, 26, 5, 241, DateTimeKind.Local).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 15, 11, 26, 5, 241, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.InsertData(
                table: "RecipesCategories",
                columns: new[] { "CategoryId", "RecipeId" },
                values: new object[,]
                {
                    { 10, -2 },
                    { 4, -1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipesCategories",
                keyColumns: new[] { "CategoryId", "RecipeId" },
                keyValues: new object[] { 10, -2 });

            migrationBuilder.DeleteData(
                table: "RecipesCategories",
                keyColumns: new[] { "CategoryId", "RecipeId" },
                keyValues: new object[] { 4, -1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "95d69c97-35d8-42e6-b6bc-e57666da8bbb", "c49b988d-1951-4f90-91e9-5b722f0badc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "567a92c9-66f2-456b-a9f0-a1f812d2631e", "19cf90ba-c894-472d-bf63-b38361e036eb" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 12, 37, 51, 463, DateTimeKind.Local).AddTicks(707));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 14, 12, 37, 51, 463, DateTimeKind.Local).AddTicks(666));
        }
    }
}

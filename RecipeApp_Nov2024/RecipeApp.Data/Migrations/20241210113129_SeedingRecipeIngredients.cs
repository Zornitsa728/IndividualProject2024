using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRecipeIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "RecipesIngredients",
                columns: new[] { "IngredientId", "RecipeId", "IsDeleted", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, 1, false, 250.0, 3 },
                    { 3, 1, false, 100.0, 0 },
                    { 1, 2, false, 100.0, 3 },
                    { 4, 2, false, 50.0, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "RecipesIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2de97f34-e3e1-4108-b88c-73cd3977b541", "d13ec444-5eb2-404f-9da4-379f09185861" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "efcb63fa-98d5-47c9-8291-208386b2cc07", "6e55b70f-ec82-431e-baad-f83401699007" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 29, 3, 27, 31, 864, DateTimeKind.Local).AddTicks(9276));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 29, 3, 27, 31, 864, DateTimeKind.Local).AddTicks(9319));
        }
    }
}

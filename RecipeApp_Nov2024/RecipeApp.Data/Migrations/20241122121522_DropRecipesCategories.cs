using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropRecipesCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipesCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "42ac7c5e-ceb2-4c6f-bc3a-bce92be6d856", "33142da5-e1f7-415a-8f53-d6e92166b5dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4305ec9c-9674-4efa-9d75-3143aea0241a", "e0397363-9e5a-470c-ad9f-5a813c077888" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CategoryId", "CreatedOn" },
                values: new object[] { 10, new DateTime(2024, 11, 22, 14, 15, 21, 732, DateTimeKind.Local).AddTicks(1031) });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CategoryId", "CreatedOn" },
                values: new object[] { 4, new DateTime(2024, 11, 22, 14, 15, 21, 732, DateTimeKind.Local).AddTicks(986) });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Categories_CategoryId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "RecipesCategories",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesCategories", x => new { x.RecipeId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RecipesCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipesCategories_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cdc829c9-6093-4666-911a-f3fe8eaf7e71", "0790c3c1-4498-4812-a6e1-ea4883ea2305" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d7920e10-931c-4cf7-881a-13f0f3990145", "1c422a9b-a58c-4131-a2f5-3121ddcf51d6" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 22, 11, 34, 47, 560, DateTimeKind.Local).AddTicks(4767));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 22, 11, 34, 47, 560, DateTimeKind.Local).AddTicks(4696));

            migrationBuilder.InsertData(
                table: "RecipesCategories",
                columns: new[] { "CategoryId", "RecipeId" },
                values: new object[,]
                {
                    { 10, -2 },
                    { 4, -1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipesCategories_CategoryId",
                table: "RecipesCategories",
                column: "CategoryId");
        }
    }
}

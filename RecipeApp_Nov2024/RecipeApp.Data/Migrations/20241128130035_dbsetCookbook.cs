using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbsetCookbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cookbook_AspNetUsers_UserId",
                table: "Cookbook");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesCookbooks_Cookbook_CookbookId",
                table: "RecipesCookbooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cookbook",
                table: "Cookbook");

            migrationBuilder.RenameTable(
                name: "Cookbook",
                newName: "Cookbooks");

            migrationBuilder.RenameIndex(
                name: "IX_Cookbook_UserId",
                table: "Cookbooks",
                newName: "IX_Cookbooks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cookbooks",
                table: "Cookbooks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "395c1cd1-bc76-40fd-ba76-990649951668", "88c9052f-c02c-4d0a-a1d3-e47c356c826c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f8475c90-7941-4e99-a628-47f504713429", "eb89a695-8dc3-4f21-a639-0e96a12b4b38" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 28, 15, 0, 33, 136, DateTimeKind.Local).AddTicks(1351));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 28, 15, 0, 33, 136, DateTimeKind.Local).AddTicks(1391));

            migrationBuilder.AddForeignKey(
                name: "FK_Cookbooks_AspNetUsers_UserId",
                table: "Cookbooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesCookbooks_Cookbooks_CookbookId",
                table: "RecipesCookbooks",
                column: "CookbookId",
                principalTable: "Cookbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cookbooks_AspNetUsers_UserId",
                table: "Cookbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesCookbooks_Cookbooks_CookbookId",
                table: "RecipesCookbooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cookbooks",
                table: "Cookbooks");

            migrationBuilder.RenameTable(
                name: "Cookbooks",
                newName: "Cookbook");

            migrationBuilder.RenameIndex(
                name: "IX_Cookbooks_UserId",
                table: "Cookbook",
                newName: "IX_Cookbook_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cookbook",
                table: "Cookbook",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6203db49-4b0e-434c-acd2-c85642aa1599", "e2975cb0-463f-4a07-b16e-8b625f29e0bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c0efac39-bfdd-4160-9dfd-06a338720492", "2d89019d-dcf4-4654-97fe-783f0c6fe275" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.AddForeignKey(
                name: "FK_Cookbook_AspNetUsers_UserId",
                table: "Cookbook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesCookbooks_Cookbook_CookbookId",
                table: "RecipesCookbooks",
                column: "CookbookId",
                principalTable: "Cookbook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

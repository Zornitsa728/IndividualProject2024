using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseInstructionsLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "nvarchar(max)",
                maxLength: 6000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3500)",
                oldMaxLength: 3500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fc7600e7-b11f-4921-8a25-40d1e995e58d", "4fd14ece-a715-4837-b448-24ab807b554b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bd16cc99-0c24-4111-b581-41c81ef6998d", "a82f30bc-d3eb-4bb5-9558-c9e2942c1841" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "nvarchar(3500)",
                maxLength: 3500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 6000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "908aea48-bfad-4f46-91d7-2ff91f9342d8", "db8fb419-6526-4f12-9f52-392149a1725f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f27ab1c6-f9bb-4828-a7d7-14a46bf8ca9a", "c0476dff-360d-45fb-a0e5-aa39de0e5207" });
        }
    }
}

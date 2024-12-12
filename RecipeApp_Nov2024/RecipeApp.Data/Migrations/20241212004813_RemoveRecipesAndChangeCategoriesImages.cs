using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRecipesAndChangeCategoriesImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ca87836-1e87-4648-803f-c4c416c5d850",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e30222c4-0f9e-462f-96ed-5bae1767baf7", "bf44bb06-cb17-4c87-8112-0b97e24f7df8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7824e012-d457-4328-ac3e-d9c792680b74", "44230084-950b-409d-8b47-658c15bf284f" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Instructions", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2024, 11, 29, 3, 27, 31, 864, DateTimeKind.Local).AddTicks(9276), "This vibrant Cranberry Pie, made with a flaky homemade pie crust and a sweet-tart cranberry filling, is the dessert perfect for the holiday season. Top a slice with vanilla ice cream, and the whole family will love this festive dessert!", "/images/Recipe/Cranberry-Pie.jpg", "1. Preheat the oven to 425°F. Let the pie dough soften on the counter so it is easier to roll out. Meanwhile, combine 5 cups of cranberries, sugar, cornstarch, orange zest, cinnamon, ginger, salt, and vanilla extract in the bowl of a food processor. Process the food processor for about 10 seconds to roughly chop the cranberries. 2. Transfer the mixture to a large bowl and fold in the remaining 1 cup of cranberries. Set aside.3. On a lightly floured surface, roll 1 disk of pie dough into a 13-inch circle (about ⅛-inch thick). Press the circle into a 9-inch deep-dish pie plate. Trim any excess dough to leave a 1-inch overhang over the edge of the pie plate. Place it in the fridge.4. Roll the other disk into a 12-inch circle. Cut the dough into ¾-inch-wide strips. There should be about 14 strips.5. Scoop the cranberry mixture into the bottom pie crust and smooth it into an even layer.6. Create a lattice pattern with the pie crust strips over the cranberries. Trim the lattice strips to have a ½-inch overhang (slightly shorter than the bottom crust). 7. Fold the bottom crust over the lattice strips and gently press down to create a flat edge. If desired, crimp the edges with your fingers. In a small bowl, beat the egg with 1 tablespoon of water.8. Use a pastry brush to apply the egg wash to the top crust, avoiding the filling. Then, place the pie on a rimmed baking sheet. Cover the edges of the pie with a pie shield or aluminum foil. Bake for 20 minutes, then reduce the oven temperature to 375°F and bake for 40-50 minutes or until the top is golden brown and the filling bubbles at the edges and in the center. Let the pie cool completely for about 4 hours before slicing.", false, "Cranberry Pie", "9ccd592c-f245-4344-b4ed-dde7df4677e1" },
                    { 2, 10, new DateTime(2024, 11, 29, 3, 27, 31, 864, DateTimeKind.Local).AddTicks(9319), "Celebrate soup season with creamy Sweet Potato Soup! This comforting soup recipe is made with sweet potatoes, warm and smoky spices, and cream, and is one of my favorite ways to warm up on a chilly day.", "/images/Recipe/Sweet-Potato-Soup.jpg", "1. In a large Dutch oven or large pot, heat the oil and butter over medium heat. Add the onion and cook, stirring occasionally, for about 5 minutes until softened. 2. Add the ginger, cumin, paprika, and garlic and cook for about 30 seconds until fragrant. 3. Add the sweet potatoes, vegetable stock, salt, and pepper. Bring to a boil over medium-high heat. Reduce the flame to medium-low heat, and simmer for 15 to 20 minutes until the sweet potatoes are very tender. 4. Ladle half of the soup into a blender. Place the lid on top, but remove the center insert to allow the steam to vent. Cover the opening loosely with a paper towel or clean dish towel and blend at medium speed for about 1 minute or smooth. Transfer the soup to a clean bowl and repeat with the remaining soup. 5. Return the soup to the pot and place over medium-low heat. 6. Stir in the heavy cream. Rewarm for 2 to 3 minutes, stirring occasionally. Garnish the soup with cream, olive oil, and freshly cracked black pepper before serving hot!", false, "Sweet Potato Soup", "6ca87836-1e87-4648-803f-c4c416c5d850" }
                });
        }
    }
}

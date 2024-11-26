using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTestRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: -1);

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

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Instructions", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7870), "This vibrant Cranberry Pie, made with a flaky homemade pie crust and a sweet-tart cranberry filling, is the dessert perfect for the holiday season. Top a slice with vanilla ice cream, and the whole family will love this festive dessert!", "/images/Recipe/Cranberry-Pie.jpg", "1. Preheat the oven to 425°F. Let the pie dough soften on the counter so it is easier to roll out. Meanwhile, combine 5 cups of cranberries, sugar, cornstarch, orange zest, cinnamon, ginger, salt, and vanilla extract in the bowl of a food processor. Process the food processor for about 10 seconds to roughly chop the cranberries. 2. Transfer the mixture to a large bowl and fold in the remaining 1 cup of cranberries. Set aside.3. On a lightly floured surface, roll 1 disk of pie dough into a 13-inch circle (about ⅛-inch thick). Press the circle into a 9-inch deep-dish pie plate. Trim any excess dough to leave a 1-inch overhang over the edge of the pie plate. Place it in the fridge.4. Roll the other disk into a 12-inch circle. Cut the dough into ¾-inch-wide strips. There should be about 14 strips.5. Scoop the cranberry mixture into the bottom pie crust and smooth it into an even layer.6. Create a lattice pattern with the pie crust strips over the cranberries. Trim the lattice strips to have a ½-inch overhang (slightly shorter than the bottom crust). 7. Fold the bottom crust over the lattice strips and gently press down to create a flat edge. If desired, crimp the edges with your fingers. In a small bowl, beat the egg with 1 tablespoon of water.8. Use a pastry brush to apply the egg wash to the top crust, avoiding the filling. Then, place the pie on a rimmed baking sheet. Cover the edges of the pie with a pie shield or aluminum foil. Bake for 20 minutes, then reduce the oven temperature to 375°F and bake for 40-50 minutes or until the top is golden brown and the filling bubbles at the edges and in the center. Let the pie cool completely for about 4 hours before slicing.", false, "Cranberry Pie", "9ccd592c-f245-4344-b4ed-dde7df4677e1" },
                    { 2, 10, new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7941), "Celebrate soup season with creamy Sweet Potato Soup! This comforting soup recipe is made with sweet potatoes, warm and smoky spices, and cream, and is one of my favorite ways to warm up on a chilly day.", "/images/Recipe/Sweet-Potato-Soup.jpg", "1. In a large Dutch oven or large pot, heat the oil and butter over medium heat. Add the onion and cook, stirring occasionally, for about 5 minutes until softened. 2. Add the ginger, cumin, paprika, and garlic and cook for about 30 seconds until fragrant. 3. Add the sweet potatoes, vegetable stock, salt, and pepper. Bring to a boil over medium-high heat. Reduce the flame to medium-low heat, and simmer for 15 to 20 minutes until the sweet potatoes are very tender. 4. Ladle half of the soup into a blender. Place the lid on top, but remove the center insert to allow the steam to vent. Cover the opening loosely with a paper towel or clean dish towel and blend at medium speed for about 1 minute or smooth. Transfer the soup to a clean bowl and repeat with the remaining soup. 5. Return the soup to the pot and place over medium-low heat. 6. Stir in the heavy cream. Rewarm for 2 to 3 minutes, stirring occasionally. Garnish the soup with cream, olive oil, and freshly cracked black pepper before serving hot!", false, "Sweet Potato Soup", "6ca87836-1e87-4648-803f-c4c416c5d850" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "1b7f10f0-794f-4eec-93ba-259e8643cd15", "6ce8dfb1-739f-4012-8772-71ba8fc92538" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "33971ee5-150b-49fc-83b5-76c5f9bafe6d", "e66d5463-06de-41dc-9860-d87104e81218" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "Instructions", "IsDeleted", "Title", "UserId" },
                values: new object[,]
                {
                    { -2, 10, new DateTime(2024, 11, 25, 16, 5, 25, 107, DateTimeKind.Local).AddTicks(6089), "Celebrate soup season with creamy Sweet Potato Soup! This comforting soup recipe is made with sweet potatoes, warm and smoky spices, and cream, and is one of my favorite ways to warm up on a chilly day.", "/images/Recipe/Sweet-Potato-Soup.jpg", "1. In a large Dutch oven or large pot, heat the oil and butter over medium heat. Add the onion and cook, stirring occasionally, for about 5 minutes until softened. 2. Add the ginger, cumin, paprika, and garlic and cook for about 30 seconds until fragrant. 3. Add the sweet potatoes, vegetable stock, salt, and pepper. Bring to a boil over medium-high heat. Reduce the flame to medium-low heat, and simmer for 15 to 20 minutes until the sweet potatoes are very tender. 4. Ladle half of the soup into a blender. Place the lid on top, but remove the center insert to allow the steam to vent. Cover the opening loosely with a paper towel or clean dish towel and blend at medium speed for about 1 minute or smooth. Transfer the soup to a clean bowl and repeat with the remaining soup. 5. Return the soup to the pot and place over medium-low heat. 6. Stir in the heavy cream. Rewarm for 2 to 3 minutes, stirring occasionally. Garnish the soup with cream, olive oil, and freshly cracked black pepper before serving hot!", false, "Sweet Potato Soup", "6ca87836-1e87-4648-803f-c4c416c5d850" },
                    { -1, 4, new DateTime(2024, 11, 25, 16, 5, 25, 107, DateTimeKind.Local).AddTicks(6034), "This vibrant Cranberry Pie, made with a flaky homemade pie crust and a sweet-tart cranberry filling, is the dessert perfect for the holiday season. Top a slice with vanilla ice cream, and the whole family will love this festive dessert!", "/images/Recipe/Cranberry-Pie.jpg", "1. Preheat the oven to 425°F. Let the pie dough soften on the counter so it is easier to roll out. Meanwhile, combine 5 cups of cranberries, sugar, cornstarch, orange zest, cinnamon, ginger, salt, and vanilla extract in the bowl of a food processor. Process the food processor for about 10 seconds to roughly chop the cranberries. 2. Transfer the mixture to a large bowl and fold in the remaining 1 cup of cranberries. Set aside.3. On a lightly floured surface, roll 1 disk of pie dough into a 13-inch circle (about ⅛-inch thick). Press the circle into a 9-inch deep-dish pie plate. Trim any excess dough to leave a 1-inch overhang over the edge of the pie plate. Place it in the fridge.4. Roll the other disk into a 12-inch circle. Cut the dough into ¾-inch-wide strips. There should be about 14 strips.5. Scoop the cranberry mixture into the bottom pie crust and smooth it into an even layer.6. Create a lattice pattern with the pie crust strips over the cranberries. Trim the lattice strips to have a ½-inch overhang (slightly shorter than the bottom crust). 7. Fold the bottom crust over the lattice strips and gently press down to create a flat edge. If desired, crimp the edges with your fingers. In a small bowl, beat the egg with 1 tablespoon of water.8. Use a pastry brush to apply the egg wash to the top crust, avoiding the filling. Then, place the pie on a rimmed baking sheet. Cover the edges of the pie with a pie shield or aluminum foil. Bake for 20 minutes, then reduce the oven temperature to 375°F and bake for 40-50 minutes or until the top is golden brown and the filling bubbles at the edges and in the center. Let the pie cool completely for about 4 hours before slicing.", false, "Cranberry Pie", "9ccd592c-f245-4344-b4ed-dde7df4677e1" }
                });
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeApp.Data;

#nullable disable

namespace RecipeApp.Data.Migrations
{
    [DbContext(typeof(RecipeDbContext))]
    [Migration("20241125140908_ChangeTestRecipes")]
    partial class ChangeTestRecipes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RecipeApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "9ccd592c-f245-4344-b4ed-dde7df4677e1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c0efac39-bfdd-4160-9dfd-06a338720492",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2d89019d-dcf4-4654-97fe-783f0c6fe275",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "6ca87836-1e87-4648-803f-c4c416c5d850",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6203db49-4b0e-434c-acd2-c85642aa1599",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e2975cb0-463f-4a07-b16e-8b625f29e0bd",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Appetizers"
                        },
                        new
                        {
                            Id = 2,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Bread Recipes"
                        },
                        new
                        {
                            Id = 3,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Breakfast"
                        },
                        new
                        {
                            Id = 4,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Desserts"
                        },
                        new
                        {
                            Id = 5,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Drinks"
                        },
                        new
                        {
                            Id = 6,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Main Dishes"
                        },
                        new
                        {
                            Id = 7,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Salads"
                        },
                        new
                        {
                            Id = 8,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Sandwiches"
                        },
                        new
                        {
                            Id = 9,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Side Dishes"
                        },
                        new
                        {
                            Id = 10,
                            ImageUrl = "/images/Category/default.jpg",
                            Name = "Soups"
                        });
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Cookbook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cookbook");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2100)
                        .HasColumnType("nvarchar(2100)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 4,
                            CreatedOn = new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7870),
                            Description = "This vibrant Cranberry Pie, made with a flaky homemade pie crust and a sweet-tart cranberry filling, is the dessert perfect for the holiday season. Top a slice with vanilla ice cream, and the whole family will love this festive dessert!",
                            ImageUrl = "/images/Recipe/Cranberry-Pie.jpg",
                            Instructions = "1. Preheat the oven to 425°F. Let the pie dough soften on the counter so it is easier to roll out. Meanwhile, combine 5 cups of cranberries, sugar, cornstarch, orange zest, cinnamon, ginger, salt, and vanilla extract in the bowl of a food processor. Process the food processor for about 10 seconds to roughly chop the cranberries. 2. Transfer the mixture to a large bowl and fold in the remaining 1 cup of cranberries. Set aside.3. On a lightly floured surface, roll 1 disk of pie dough into a 13-inch circle (about ⅛-inch thick). Press the circle into a 9-inch deep-dish pie plate. Trim any excess dough to leave a 1-inch overhang over the edge of the pie plate. Place it in the fridge.4. Roll the other disk into a 12-inch circle. Cut the dough into ¾-inch-wide strips. There should be about 14 strips.5. Scoop the cranberry mixture into the bottom pie crust and smooth it into an even layer.6. Create a lattice pattern with the pie crust strips over the cranberries. Trim the lattice strips to have a ½-inch overhang (slightly shorter than the bottom crust). 7. Fold the bottom crust over the lattice strips and gently press down to create a flat edge. If desired, crimp the edges with your fingers. In a small bowl, beat the egg with 1 tablespoon of water.8. Use a pastry brush to apply the egg wash to the top crust, avoiding the filling. Then, place the pie on a rimmed baking sheet. Cover the edges of the pie with a pie shield or aluminum foil. Bake for 20 minutes, then reduce the oven temperature to 375°F and bake for 40-50 minutes or until the top is golden brown and the filling bubbles at the edges and in the center. Let the pie cool completely for about 4 hours before slicing.",
                            IsDeleted = false,
                            Title = "Cranberry Pie",
                            UserId = "9ccd592c-f245-4344-b4ed-dde7df4677e1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 10,
                            CreatedOn = new DateTime(2024, 11, 25, 16, 9, 7, 872, DateTimeKind.Local).AddTicks(7941),
                            Description = "Celebrate soup season with creamy Sweet Potato Soup! This comforting soup recipe is made with sweet potatoes, warm and smoky spices, and cream, and is one of my favorite ways to warm up on a chilly day.",
                            ImageUrl = "/images/Recipe/Sweet-Potato-Soup.jpg",
                            Instructions = "1. In a large Dutch oven or large pot, heat the oil and butter over medium heat. Add the onion and cook, stirring occasionally, for about 5 minutes until softened. 2. Add the ginger, cumin, paprika, and garlic and cook for about 30 seconds until fragrant. 3. Add the sweet potatoes, vegetable stock, salt, and pepper. Bring to a boil over medium-high heat. Reduce the flame to medium-low heat, and simmer for 15 to 20 minutes until the sweet potatoes are very tender. 4. Ladle half of the soup into a blender. Place the lid on top, but remove the center insert to allow the steam to vent. Cover the opening loosely with a paper towel or clean dish towel and blend at medium speed for about 1 minute or smooth. Transfer the soup to a clean bowl and repeat with the remaining soup. 5. Return the soup to the pot and place over medium-low heat. 6. Stir in the heavy cream. Rewarm for 2 to 3 minutes, stirring occasionally. Garnish the soup with cream, olive oil, and freshly cracked black pepper before serving hot!",
                            IsDeleted = false,
                            Title = "Sweet Potato Soup",
                            UserId = "6ca87836-1e87-4648-803f-c4c416c5d850"
                        });
                });

            modelBuilder.Entity("RecipeApp.Data.Models.RecipeCookbook", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("CookbookId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "CookbookId");

                    b.HasIndex("CookbookId");

                    b.ToTable("RecipesCookbooks");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double>("Quantity")
                        .HasMaxLength(1000)
                        .HasColumnType("float");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipesIngredients");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Comment", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Cookbook", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Rating", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.Recipe", "Recipe")
                        .WithMany("Ratings")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Recipe", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.ApplicationUser", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.RecipeCookbook", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.Cookbook", "Cookbook")
                        .WithMany("RecipeCookbooks")
                        .HasForeignKey("CookbookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeCookbooks")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cookbook");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.RecipeIngredient", b =>
                {
                    b.HasOne("RecipeApp.Data.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RecipeApp.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Cookbook", b =>
                {
                    b.Navigation("RecipeCookbooks");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("RecipeApp.Data.Models.Recipe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");

                    b.Navigation("RecipeCookbooks");

                    b.Navigation("RecipeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}

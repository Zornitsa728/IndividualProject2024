using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(config =>
       {
           ConfigureIdentity(config);
       })
       .AddEntityFrameworkStores<RecipeDbContext>()
       .AddRoles<IdentityRole>()
       .AddSignInManager<SignInManager<ApplicationUser>>()
       .AddUserManager<UserManager<ApplicationUser>>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//This ensures all models in the Data.Models are automatically configured with their corresponding repositories
var modelsAssembly = typeof(Recipe).Assembly;
// Create a scope to access the DbContext
using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    builder.Services.RegisterRepositories(modelsAssembly, dbContext);
}

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    RecipeDbContext context = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    var services = scope.ServiceProvider;

    DatabaseSeeder.SeedIngredientsFromJson(context);
    DatabaseSeeder.SeedRoles(services);
    DatabaseSeeder.AssignAdminRole(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.Use((context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true && context.Request.Path == "/")
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Response.Redirect("/Admin/Home/Index");
            return Task.CompletedTask;
        }
    }
    return next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

//in real project this should be in manage user secrets
// private is not needed because methods are scoped to the file and behave as private by default
static void ConfigureIdentity(IdentityOptions config)
{
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequiredLength = 3;
    config.Password.RequiredUniqueChars = 0;

    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    config.Lockout.MaxFailedAccessAttempts = 5;
    config.Lockout.AllowedForNewUsers = true;

    config.SignIn.RequireConfirmedAccount = false;
    config.SignIn.RequireConfirmedEmail = false;
    config.SignIn.RequireConfirmedPhoneNumber = false;

    config.User.RequireUniqueEmail = true;
}


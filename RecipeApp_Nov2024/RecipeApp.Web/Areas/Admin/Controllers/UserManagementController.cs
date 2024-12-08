using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.AdminViewModels.UserManagementViewModels;

namespace RecipeApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUser> allUsers = await this.userManager
                .Users
                .ToArrayAsync();

            List<AllUsersViewModel> userModel = new List<AllUsersViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await userManager.GetRolesAsync(user);
                userModel.Add(new AllUsersViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user != null && await roleManager.RoleExistsAsync(role))
            {
                await userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user != null && await roleManager.RoleExistsAsync(role))
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


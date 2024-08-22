using app.Areas.Admin.ViewModel.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            List<UserRolesVM> userRoles = new();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                string rolesStr = string.Join(",", roles.ToArray());
                userRoles.Add(new UserRolesVM
                {
                    FullName = user.FullName,
                    Roles = rolesStr,
                    Username = user.UserName,
                    Email = user.Email
                });
            }
            return View(userRoles);
        }
    }
}

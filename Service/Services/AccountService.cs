
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Services.Interfaces;
using Service.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<SignInResult> Login(SignInVM request)
        {
           var user = await _userManager.FindByEmailAsync(request.EmailOrUserName) ?? await _userManager.FindByNameAsync(request.EmailOrUserName);
            if (user == null) {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

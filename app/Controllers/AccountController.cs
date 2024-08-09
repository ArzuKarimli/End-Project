using Domain.Entities;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Service.Helpers;
using Service.Services.Interfaces;
using Service.ViewModel.Account;
using System.Net.Mail;
using System.Security.AccessControl;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace app.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _LoginManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> LoginManager, RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            _userManager = userManager;
            _LoginManager = LoginManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<IActionResult> SignUp(RegisterVM request)
        {
            AppUser user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                foreach (var role in Enum.GetValues(typeof(Roles)))
                {
                    if (!await _roleManager.RoleExistsAsync(nameof(Roles.Member)))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(nameof(Roles.Member)));
                    }
                }
                await _userManager.AddToRoleAsync(user, nameof(Roles.Member));

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                string action = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("arzusk@code.edu.az"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Confirmation";
                email.Body = new TextPart(TextFormat.Html) { Text = $"<a href=\"{action}\">Click Here</a>" };


                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("arzusk@code.edu.az", "pmwr wdrq jvgu ncyc");
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            return RedirectToAction(nameof(VerifyEmail));
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(SignIn));
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accountService.Login(request);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}


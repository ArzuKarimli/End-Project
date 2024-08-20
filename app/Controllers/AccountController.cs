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
        private readonly ITeacherService _teacherService;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> LoginManager, RoleManager<IdentityRole> roleManager, IAccountService accountService, ITeacherService teacherService)
        {
            _userManager = userManager;
            _LoginManager = LoginManager;
            _roleManager = roleManager;
            _accountService = accountService;
            _teacherService = teacherService;
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
                FullName = request.FullName,
                Specialization = request.IsTeacher ? request.Specialization : null
            };
            if (request.IsTeacher && string.IsNullOrEmpty(request.Specialization))
            {
                ModelState.AddModelError("Specialization", "Specialization is required for Teachers.");
                return View(request);
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)              
            {
                if (request.IsTeacher)
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                    user.Specialization = request.Specialization;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Member"); 
                }
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
                smtp.Authenticate("arzusk@code.edu.az", "zxfg txje viix nnmf");
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

            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> RegisterTeacher(RegisterTeacherVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Specialization = request.Specialization,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(nameof(Roles.Teacher)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(nameof(Roles.Teacher)));
                }

                await _userManager.AddToRoleAsync(user, nameof(Roles.Teacher));

              
                var teacher = new Teacher
                {
                    FullName = request.FullName,
                    Image = "", 
                    IsMain = false
                };
                await _teacherService.CreateAsync(teacher);

              
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string action = Url.Action(nameof(ApproveTeacher), "Account", new { username = request.Username, email = request.Email, fullName = request.FullName, specialization = request.Specialization }, Request.Scheme);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("arzusk@code.edu.az"));
                email.To.Add(MailboxAddress.Parse("arzusk@code.edu.az"));
                email.Subject = "Teacher Registration";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $@"
        <h3>Application to join the course as a Teacher</h3>
        <p><strong>Username:</strong> {request.Username}</p>
        <p><strong>Email:</strong> {request.Email}</p>
        <p><strong>Full Name:</strong> {request.FullName}</p>
        <p><strong>Specialization:</strong> {request.Specialization}</p>
        <p><a href='{action}'>Click here to approve this teacher</a></p>"
                };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("arzusk@code.edu.az", "zxfg txje viix nnmf");
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }

                return RedirectToAction(nameof(VerifyEmail));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(request);
        }



        [HttpGet]
        public async Task<IActionResult> ApproveTeacher(string username, string email, string fullName, string specialization)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Content("Teacher not found.");
            }

            if (!await _roleManager.RoleExistsAsync(nameof(Roles.Teacher)))
            {
                await _roleManager.CreateAsync(new IdentityRole(nameof(Roles.Teacher)));
            }

            await _userManager.AddToRoleAsync(user, nameof(Roles.Teacher));

         
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string action = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);

            var confirmationEmail = new MimeMessage();
            confirmationEmail.From.Add(MailboxAddress.Parse("arzusk@code.edu.az"));
            confirmationEmail.To.Add(MailboxAddress.Parse(email));
            confirmationEmail.Subject = "Your Registration has been Approved";
            confirmationEmail.Body = new TextPart(TextFormat.Html)
            {
                Text = $@"
            <h3>Your registration as a Teacher has been approved!</h3>
            <p><a href='{action}'>Click here</a> to confirm your email and complete your registration.</p>"
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("arzusk@code.edu.az", "zxfg txje viix nnmf");
                await smtp.SendAsync(confirmationEmail);
                smtp.Disconnect(true);
            }

            return RedirectToAction("Index","Home"); 
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
               
                ModelState.AddModelError(string.Empty, "Email could not be found or is not confirmed.");
                return View();
            }
        
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);         
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse("arzusk@code.edu.az"));
            emailMessage.To.Add(MailboxAddress.Parse(user.Email));
            emailMessage.Subject = "Password Reset";
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = $"Please reset your password by clicking <a href='{resetLink}'>here</a>."
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("arzusk@code.edu.az", "zxfg txje viix nnmf");
                await smtp.SendAsync(emailMessage);
                smtp.Disconnect(true);
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid request.");
                return View();
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }


}


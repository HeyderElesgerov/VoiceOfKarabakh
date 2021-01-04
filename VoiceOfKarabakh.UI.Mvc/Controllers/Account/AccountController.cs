using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.Interfaces.Email;
using VoiceOfKarabakh.Application.ViewModels.Account;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSenderService _emailSenderService;
        const string Admin = "Admin";

        public AccountController(UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSenderService emailSenderService)
        {
            _usermanager = UserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSenderService = emailSenderService;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public IActionResult ResetPasswordIntent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordIntent([Required]string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanager.FindByEmailAsync(email);

                if(user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View();
                }

                var token = await _usermanager.GeneratePasswordResetTokenAsync(user);
                string url = Url.Action(
                                action: nameof(ResetPassword),
                                controller: "Account",
                                values: new { email = email, token = token },
                                protocol: "https");

                string message = "Click on this link to reset password of your account: " + url;

                _emailSenderService.Send(email, "Password reset confirmation by VOK", message);
                return RedirectToAction(nameof(ConfirmationSentInfo), new { email = email});
            }

            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel()
            {
                Email = email,
                Token = token
            };

            return View(resetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = resetPasswordViewModel.Email;
                string token = resetPasswordViewModel.Token;

                var user = await _usermanager.FindByEmailAsync(email);

                if(user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View();
                }

                var result = await _usermanager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(PasswordChangedInfo));
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = loginViewModel.Email;
                string password = loginViewModel.Password;
                bool rememberMe = loginViewModel.RememberMe;

                var user = await _usermanager.FindByEmailAsync(email);

                if (user == null)
                {
                    ModelState.AddModelError("", "User not found");
                    return View(loginViewModel);
                }

                var signInResult = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("", "Email or password is incorrect");
                    return View(loginViewModel);
                }

                if (await _usermanager.IsInRoleAsync(user, Admin))
                {
                    return RedirectToAction("AdminIndex", "Home");
                }

                return RedirectToAction("Index", "Home");
            }

            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(registerViewModel.Email);
                user.Email = registerViewModel.Email;
                
                var result = await _usermanager.CreateAsync(user, registerViewModel.Password);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(registerViewModel);
                }

                var token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);

                string url = Url.Action(
                                action: nameof(ConfirmAccount),
                                controller: "Account",
                                values: new { email = registerViewModel.Email, token = token },
                                protocol: "https");

                string message = "Click on this link to confirm your account: "+url;

                _emailSenderService.Send(registerViewModel.Email, "Email confirmation by VOK", message);
                return RedirectToAction(nameof(ConfirmationSentInfo), new { email = registerViewModel.Email });
            }

            return View(registerViewModel);
        }

        public IActionResult ConfirmationSentInfo(string email)
        {
            return View(model: email);
        }

        public async Task<IActionResult> ConfirmAccount(string email, string token)
        {
            var user = await _usermanager.FindByEmailAsync(email);

            if (user != null)
            {
                var confirmationResult = await _usermanager.ConfirmEmailAsync(user, token);

                if (confirmationResult.Succeeded)
                {
                    return View(nameof(AccountConfirmedInfo));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult AccountConfirmedInfo()
        {
            return View();
        }
        public IActionResult PasswordChangedInfo()
        {
            return View();
        }
    }
}

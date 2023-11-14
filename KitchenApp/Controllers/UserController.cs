using KitchenApp.Entities;
using KitchenApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitchenApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new();

                try
                {
                    user = await _userManager.FindByEmailAsync(signInViewModel.Email);
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "E-posta veya parola hatalı.");
                    return View(signInViewModel);
                }

                if (user.Id != 0)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, false, true);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        var returnUrl = TempData["ReturnUrl"];

                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl.ToString() ?? "/");
                        }
                        return RedirectToAction("Add", "Item");

                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutEndUtc = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutEndUtc.Value - DateTime.UtcNow;

                        ModelState.AddModelError(string.Empty, $"Hesabınız kitlendi. Lütfen {timeLeft.Minutes} dakika sonra tekrar deneyiniz.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "E-posta veya şifre yanlış.");
                    }
                }
            }

            return View(signInViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = signUpViewModel.UserName,
                    Name = signUpViewModel.Name,
                    Surname = signUpViewModel.Surname,
                    Email = signUpViewModel.Email,
                    BirthDate = signUpViewModel.BirthDate
                };

                var creationAttempt = await _userManager.CreateAsync(appUser, signUpViewModel.Password);

                if (creationAttempt.Succeeded)
                {
                    return RedirectToAction("Login", "User");
                }

                creationAttempt.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });

            }

            return View(signUpViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Home", "Index");
        }
    }
}

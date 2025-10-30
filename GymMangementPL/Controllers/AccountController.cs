using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.AccountViewModels;
using GymMangmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymMangementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountViewModel accountView)
        {
            if (!ModelState.IsValid)
            {
                return View(accountView);
            }
            var user = _accountService.ValidateUser(accountView);
            if (user is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email or Password");
                return View(accountView);
            }
            var Result = _signInManager.PasswordSignInAsync(user, accountView.Password, accountView.RememberMe, false).Result;
            if (Result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account Not Allowed");
            if (Result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account is Locked out");
            if (Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(accountView);


        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

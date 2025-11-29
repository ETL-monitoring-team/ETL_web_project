using ETL_web_project.Data.Context;
using ETL_web_project.DTOs.Account;
using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETL_web_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ProjectContext _context;

        public AccountController(IAccountService accountService, ProjectContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginDto());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userDto = await _accountService.ValidateUserAsync(model);

            if (userDto == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.Role, userDto.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _accountService.UsernameExistsAsync(model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            if (await _accountService.EmailExistsAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(model);
            }

            var createdUser = await _accountService.RegisterUserAsync(model);

            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            TempData["Status"] = "If the email exists, the admin has been notified.";

            var token = await _accountService.GeneratePasswordResetTokenAsync(email);

            if (token != null)
            {
                var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);

                ViewBag.Username = user?.Username;
                ViewBag.UserEmail = user?.Email;

                ViewBag.ResetLink = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = token },
                    Request.Scheme);
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            return View(new ResetPasswordDto { Token = token });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _accountService.ResetPasswordAsync(dto.Token, dto.NewPassword);

            if (!success)
            {
                ModelState.AddModelError("", "Token is invalid or expired.");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

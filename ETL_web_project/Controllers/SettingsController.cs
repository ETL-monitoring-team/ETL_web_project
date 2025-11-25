using System.Security.Claims;
using ETL_web_project.DTOs;
using ETL_web_project.Interfaces;
using ETL_web_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETL_web_project.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _service;

        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = await _service.GetSettingsForUserAsync(GetUserId());
            TempData["ActiveTab"] = "profile";
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ProfileSettingsDto profile)
        {
            var userId = GetUserId();
            profile.UserId = userId;

            if (string.IsNullOrWhiteSpace(profile.ConfirmPassword))
            {
                ModelState.AddModelError("Profile.ConfirmPassword", "Password is required.");
            }

            if (!ModelState.IsValid)
            {
                TempData["ActiveTab"] = "profile";
                var vm = await _service.GetSettingsForUserAsync(userId);
                vm.Profile.Username = profile.Username;
                vm.Profile.Email = profile.Email;
                return View("Index", vm);
            }

            var result = await _service.UpdateProfileAsync(profile);

            if (!result.Success)
            {
                TempData["ActiveTab"] = "profile";
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                var vm = await _service.GetSettingsForUserAsync(userId);
                vm.Profile.Username = profile.Username;
                vm.Profile.Email = profile.Email;
                return View("Index", vm);
            }

            TempData["ActiveTab"] = "profile";
            TempData["ProfileSuccess"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var userId = GetUserId();
            model.UserId = userId;

            if (!ModelState.IsValid)
            {
                TempData["ActiveTab"] = "password";
                var vm = await _service.GetSettingsForUserAsync(userId);
                vm.PasswordModel = model;
                return View("Index", vm);
            }

            var result = await _service.ChangePasswordAsync(model);

            if (!result.Success)
            {
                TempData["ActiveTab"] = "password";
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                var vm = await _service.GetSettingsForUserAsync(userId);
                vm.PasswordModel = model;
                return View("Index", vm);
            }

            TempData["ActiveTab"] = "password";
            TempData["ProfileSuccess"] = "Password updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreferences(UserPreferenceDto prefs)
        {
            var userId = GetUserId();
            prefs.UserId = userId;

            TempData["ActiveTab"] = "prefs";

            var result = await _service.UpdatePreferencesAsync(prefs);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                var vm = await _service.GetSettingsForUserAsync(userId);
                vm.Preferences = prefs;
                return View("Index", vm);
            }

            TempData["ProfileSuccess"] = "Preferences updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}

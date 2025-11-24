using System.Security.Claims;
using ETL_web_project.DTOs;
using ETL_web_project.Interfaces;
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
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(idStr);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = await _service.GetSettingsForUserAsync(GetUserId());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ProfileSettingsDto dto)
        {
            dto.UserId = GetUserId();

            if (!ModelState.IsValid)
            {
                var vmInvalid = await _service.GetSettingsForUserAsync(GetUserId());
                vmInvalid.Profile.Username = dto.Username;
                vmInvalid.Profile.Email = dto.Email;

                TempData["SettingsError"] = "Please fix validation errors and try again.";
                return View("Index", vmInvalid);
            }

            var ok = await _service.UpdateProfileAsync(dto);
            if (!ok)
            {
                TempData["SettingsError"] = "Profile could not be updated.";
            }
            else
            {
                TempData["SettingsMessage"] = "Profile updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            dto.UserId = GetUserId();

            if (!ModelState.IsValid)
            {
                var vmInvalid = await _service.GetSettingsForUserAsync(GetUserId());
                return View("Index", vmInvalid);
            }

            var ok = await _service.ChangePasswordAsync(dto);
            if (!ok)
            {
                TempData["SettingsError"] = "Current password is incorrect.";
            }
            else
            {
                TempData["SettingsMessage"] = "Password changed successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePreferences(UserPreferenceDto dto)
        {
            await _service.UpdatePreferencesAsync(GetUserId(), dto);
            TempData["SettingsMessage"] = "Preferences saved.";
            return RedirectToAction(nameof(Index));
        }
    }
}

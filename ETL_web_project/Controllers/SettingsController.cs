using ETL_web_project.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            profile.UserId = GetUserId();

            if (!ModelState.IsValid)
            {
                TempData["ActiveTab"] = "profile";

                var vm = await _service.GetSettingsForUserAsync(profile.UserId);
                vm.Profile = profile;

                return View("Index", vm);
            }

            var result = await _service.UpdateProfileAsync(profile);

            if (!result.Success)
            {
                TempData["ActiveTab"] = "profile";
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

                var vm = await _service.GetSettingsForUserAsync(profile.UserId);
                vm.Profile = profile;

                return View("Index", vm);
            }

            TempData["ProfileSuccess"] = "Profile updated successfully.";
            TempData["ActiveTab"] = "profile";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            dto.UserId = GetUserId();

            if (!ModelState.IsValid)
            {
                TempData["ActiveTab"] = "password";

                var vm = await _service.GetSettingsForUserAsync(dto.UserId);
                return View("Index", vm);
            }

            var result = await _service.ChangePasswordAsync(dto);

            if (!result.Success)
            {
                TempData["ActiveTab"] = "password";
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

                var vm = await _service.GetSettingsForUserAsync(dto.UserId);
                return View("Index", vm);
            }

            TempData["ProfileSuccess"] = "Password updated successfully.";
            TempData["ActiveTab"] = "profile";

            return RedirectToAction(nameof(Index));
        }

    }
}

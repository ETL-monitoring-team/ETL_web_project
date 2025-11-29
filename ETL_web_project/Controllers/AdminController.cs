using ETL_web_project.DTOs.Account;
using ETL_web_project.DTOs.Admin;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETL_web_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;

        public AdminController(IAdminService adminService, IAccountService accountService)
        {
            _adminService = adminService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var vm = await _adminService.GetDashboardAsync(search);
            return View(vm);
        }

        // ============= Role değiştir (şifre doğrulamalı) =============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(AdminChangeRoleDto model)
        {
            if (string.IsNullOrWhiteSpace(model.AdminPassword))
            {
                TempData["AdminError"] = "Please enter your password to confirm role change.";
                return RedirectToAction(nameof(Index));
            }

            var currentUsername = User.Identity?.Name;
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["AdminError"] = "User context not found.";
                return RedirectToAction(nameof(Index));
            }

            var loginDto = new LoginDto
            {
                Username = currentUsername,
                Password = model.AdminPassword
            };

            var adminUser = await _accountService.ValidateUserAsync(loginDto);

            if (adminUser == null || adminUser.Role != UserRole.Admin)
            {
                TempData["AdminError"] = "Password is incorrect or you are not allowed to change roles.";
                return RedirectToAction(nameof(Index));
            }

            if (!Enum.TryParse<UserRole>(model.Role, out var newRole))
            {
                TempData["AdminError"] = "Invalid role.";
                return RedirectToAction(nameof(Index));
            }

            var ok = await _adminService.ChangeUserRoleAsync(model.UserId, newRole);
            if (!ok)
            {
                TempData["AdminError"] = "User not found or role could not be updated.";
            }
            else
            {
                TempData["AdminMessage"] = "User role updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

        // ============= Aktif / Pasif =============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserActive(int userId)
        {
            var ok = await _adminService.ToggleUserActiveAsync(userId);
            if (!ok)
            {
                TempData["AdminError"] = "User not found or status could not be updated.";
            }
            else
            {
                TempData["AdminMessage"] = "User status updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETL_web_project.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int range = 14)
        {
            if (range != 7 && range != 14 && range != 30)
                range = 14;

            var model = await _dashboardService.GetDashboardAsync(range);
            return View(model);
        }
    }
}

using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETL_web_project.Controllers
{
    [Authorize]
    public class EtlController : Controller
    {
        private readonly IEtlLogService _etlLogService;
        private readonly IEtlJobService _etlJobService;
        private readonly IFactExplorerService _factExplorerService;
        private readonly IStagingService _stagingService;

        public EtlController(
            IEtlLogService etlLogService,
            IEtlJobService etlJobService,
            IFactExplorerService factExplorerService,
            IStagingService stagingService)
        {
            _etlLogService = etlLogService;
            _etlJobService = etlJobService;
            _factExplorerService = factExplorerService ?? throw new ArgumentNullException(nameof(factExplorerService));
            _stagingService = stagingService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,DataEngineer")]
        public async Task<IActionResult> Jobs(string? search)
        {
            var jobs = await _etlJobService.GetJobsAsync(search);
            ViewData["Search"] = search;
            return View(jobs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,DataEngineer")]
        public async Task<IActionResult> Logs()
        {
            var logs = await _etlLogService.GetLogsAsync(null, null, null, null);
            return View(logs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,DataEngineer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RunJob(int jobId)
        {
            var runId = await _etlJobService.TriggerRunAsync(jobId);

            TempData["JobRunMessage"] = $"Job #{jobId} manually triggered (RunId: {runId}).";

            return RedirectToAction(nameof(Jobs));
        }

        [HttpGet]
        public async Task<IActionResult> JobRuns(int jobId)
        {
            var runs = await _etlJobService.GetRunsForJobAsync(jobId);

            ViewData["JobId"] = jobId;
            return View(runs);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,DataEngineer")]
        public async Task<IActionResult> Staging()
        {
            var model = await _stagingService.GetStagingOverviewAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReloadStaging()
        {
            return RedirectToAction(nameof(Staging));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearStaging()
        {
            await _stagingService.ClearStagingAsync();
            return RedirectToAction(nameof(Staging));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportStagingCsv()
        {
            var csv = await _stagingService.ExportCsvAsync();
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);

            return File(bytes, "text/csv", "staging_export.csv");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IActionResult> Facts(DateTime? fromDate, DateTime? toDate, string? storeSearch, string? productSearch, string? customerSearch)
        {
            var model = await _factExplorerService.GetFactExplorerAsync(
                fromDate,
                toDate,
                storeSearch,
                productSearch,
                customerSearch);

            return View(model);
        }
    }
}

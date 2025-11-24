using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ETL_web_project.Interfaces;
using ETL_web_project.DTOs;   // <-- EKLENDİ

namespace ETL_web_project.Controllers
{
    [Authorize]

    public class EtlController : Controller
    {
        private readonly IEtlLogService _etlLogService;
        private readonly IEtlJobService _etlJobService;

        public EtlController(
            IEtlLogService etlLogService,
            IEtlJobService etlJobService)
        {
            _etlLogService = etlLogService;
            _etlJobService = etlJobService;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        // ============ SIDEBAR PAGES ============

        public async Task<ActionResult> Jobs(string? search)
        {
            var jobs = await _etlJobService.GetJobsAsync(search);
            ViewData["Search"] = search;          // arama terimini view'a gönder
            return View(jobs);
        }

        public async Task<ActionResult> Logs()
        {
            var logs = await _etlLogService.GetLogsAsync(null, null, null, null);
            return View(logs);
        }

        public ActionResult Staging()
        {
            // Model null gelmesin diye boş da olsa bir DTO gönderiyoruz
            var model = new StagingPageDto();
            return View(model);
        }

        public ActionResult Facts()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }

        // =======================================

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EtlController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EtlController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EtlController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EtlController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EtlController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EtlController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

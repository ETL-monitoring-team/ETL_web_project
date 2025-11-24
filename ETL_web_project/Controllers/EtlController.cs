using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ETL_web_project.Interfaces;

namespace ETL_web_project.Controllers
{
    public class EtlController : Controller
    {
        private readonly IEtlLogService _etlLogService;

        public EtlController(IEtlLogService etlLogService)
        {
            _etlLogService = etlLogService;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        // ============ SIDEBAR PAGES ============

        public ActionResult Jobs()
        {
            return View();
        }

        public async Task<ActionResult> Logs()
        {
            var logs = await _etlLogService.GetLogsAsync(null, null, null, null);
            return View(logs);
        }

        public ActionResult Staging()
        {
            return View();
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

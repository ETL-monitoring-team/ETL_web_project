using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETL_web_project.Controllers
{
    public class EtlController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //BUNLAR OLUŞTURULACAK SİDEBARDAKİ VİEWLARDAN BAZISI
        public ActionResult Jobs()
        {
            return View();
        }
        public ActionResult Logs()
        {
            return View();
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

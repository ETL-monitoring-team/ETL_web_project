using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Controllers
{
    public class EtlScheduleController : Controller
    {
        private readonly IEtlScheduleOverviewService _overviewService;
        private readonly IEtlScheduleService _scheduleService;
        private readonly ProjectContext _context; // <-- EKLENDİ

        public EtlScheduleController(
            IEtlScheduleOverviewService overviewService,
            IEtlScheduleService scheduleService,
            ProjectContext context)
        {
            _overviewService = overviewService;
            _scheduleService = scheduleService;
            _context = context; // <-- EKLENDİ
        }

        // ======================================================
        // INDEX
        // ======================================================
        public async Task<IActionResult> Index()
        {
            var overview = await _overviewService.GetOverviewAsync();
            return View(overview);
        }

        // ======================================================
        // CREATE - GET
        // ======================================================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var overview = await _overviewService.GetOverviewAsync();

            var model = new EtlScheduleCreateFormDto
            {
                AvailableJobs = overview.AvailableJobs
            };

            return View(model);
        }

        // ======================================================
        // CREATE - POST
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> Create(EtlScheduleCreateFormDto model)
        {
            if (!ModelState.IsValid)
            {
                var overview = await _overviewService.GetOverviewAsync();
                model.AvailableJobs = overview.AvailableJobs;
                return View(model);
            }

            var dto = model.CreateDto;

            var schedule = new EtlSchedule
            {
                JobId = dto.JobId,
                FrequencyText = dto.FrequencyText,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _scheduleService.CreateAsync(schedule);
            return RedirectToAction("Index");
        }

        // ======================================================
        // EDIT - GET
        // ======================================================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            var jobs = await _context.EtlJobs
                .OrderBy(j => j.JobName)
                .ToListAsync();

            ViewBag.AvailableJobs = jobs;

            var dto = new EtlScheduleEditDto
            {
                ScheduleId = schedule.ScheduleId,
                JobId = schedule.JobId,
                FrequencyText = schedule.FrequencyText,
                IsActive = schedule.IsActive
            };

            return View(dto);
        }

        // ======================================================
        // EDIT - POST
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> Edit(EtlScheduleEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                var jobs = await _context.EtlJobs
                    .OrderBy(j => j.JobName)
                    .ToListAsync();

                ViewBag.AvailableJobs = jobs;
                return View(dto);
            }

            var entity = await _scheduleService.GetByIdAsync(dto.ScheduleId);
            if (entity == null)
                return NotFound();

            entity.JobId = dto.JobId;
            entity.FrequencyText = dto.FrequencyText;
            entity.IsActive = dto.IsActive;

            await _scheduleService.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        // ======================================================
        // PAUSE / RESUME
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id)
        {
            await _scheduleService.ToggleActiveAsync(id);
            return RedirectToAction("Index");
        }

        // ======================================================
        // RUN NOW
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> RunNow(int id)
        {
            await _scheduleService.RunNowAsync(id);
            return RedirectToAction("Index");
        }
    }
}

using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ProjectContext _context;

        public DashboardService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardAsync()
        {
            var now = DateTime.UtcNow;
            var today = now.Date;
            var yesterday = today.AddDays(-1);
            var fromDate14 = today.AddDays(-13);   // son 14 gün için

            var dto = new DashboardSummaryDto();

            // =========================================
            // 1) SALES KPI'lari (FactSales + DimDate)
            // =========================================

            var factQuery = _context.FactSales
                                    .Include(f => f.Date)
                                    .AsQueryable();

            // TOTAL
            dto.TotalSalesAmount =
                await factQuery.SumAsync(f => (decimal?)f.TotalAmount) ?? 0m;

            dto.TotalSalesQuantity =
                await factQuery.SumAsync(f => (int?)f.Quantity) ?? 0;

            // TODAY
            var todayFacts = factQuery.Where(f => f.Date.Date == today);

            dto.TodaySalesAmount =
                await todayFacts.SumAsync(f => (decimal?)f.TotalAmount) ?? 0m;

            dto.TodaySalesQuantity =
                await todayFacts.SumAsync(f => (int?)f.Quantity) ?? 0;

            // YESTERDAY
            var yestFacts = factQuery.Where(f => f.Date.Date == yesterday);

            var yesterdayAmount =
                await yestFacts.SumAsync(f => (decimal?)f.TotalAmount) ?? 0m;

            if (yesterdayAmount == 0)
            {
                dto.TodayVsYesterdaySalesPercent = 0;
            }
            else
            {
                dto.TodayVsYesterdaySalesPercent =
                    (dto.TodaySalesAmount - yesterdayAmount) / yesterdayAmount * 100m;
            }

            // =========================================
            // 2) DATA FRESHNESS
            // =========================================

            // Warehouse (FactSales.CreatedAt)
            dto.WarehouseLastLoadTime =
                await factQuery.MaxAsync(f => (DateTime?)f.CreatedAt);

            dto.WarehouseFreshnessHours =
                dto.WarehouseLastLoadTime.HasValue
                    ? (int?)Math.Round((now - dto.WarehouseLastLoadTime.Value).TotalHours)
                    : null;

            // Staging (SalesRaw.LoadedAt)
            var rawQuery = _context.SalesRaws.AsQueryable();

            dto.StagingLastLoadTime =
                await rawQuery.MaxAsync(r => (DateTime?)r.LoadedAt);

            dto.StagingFreshnessHours =
                dto.StagingLastLoadTime.HasValue
                    ? (int?)Math.Round((now - dto.StagingLastLoadTime.Value).TotalHours)
                    : null;

            // =========================================
            // 3) ETL RUN SUMMARY
            // =========================================

            var runQuery = _context.EtlRuns
                                   .Include(r => r.Job)
                                   .AsQueryable();

            var lastRun = await runQuery
                .OrderByDescending(r => r.StartTime)
                .FirstOrDefaultAsync();

            if (lastRun != null)
            {
                dto.LastEtlStatus = lastRun.Status;
                dto.LastEtlStartTime = lastRun.StartTime;
                dto.LastEtlEndTime = lastRun.EndTime;

                if (lastRun.EndTime.HasValue)
                {
                    var duration = lastRun.EndTime.Value - lastRun.StartTime;
                    dto.LastEtlDurationText = duration.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    dto.LastEtlDurationText = "Running";
                }
            }

            dto.FailedRunsLast24Hours = await runQuery
                .Where(r => r.Status == EtlStatus.Failed &&
                            r.StartTime >= now.AddHours(-24))
                .CountAsync();

            dto.ActiveJobCount = await _context.EtlJobs
                .CountAsync(j => j.IsActive);

            dto.RecentRuns = await runQuery
                .OrderByDescending(r => r.StartTime)
                .Take(5)
                .Select(r => new RecentEtlRunDto
                {
                    JobName = r.Job.JobName,
                    Status = r.Status,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                })
                .ToListAsync();


            // =========================================
            // 4) DAILY SALES SERIES (LAST 14 DAYS)
            // =========================================

            dto.DailySales = await factQuery
                .Where(f => f.Date.Date >= fromDate14 &&
                            f.Date.Date <= today)
                .GroupBy(f => f.Date.Date)
                .Select(g => new DailySalesPointDto
                {
                    Date = g.Key,
                    Amount = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();


            // =========================================
            // 5) TOP STORES & PRODUCTS
            // =========================================

            dto.TopStores = await _context.FactSales
                .Include(f => f.Store)
                .GroupBy(f => new { f.StoreKey, f.Store.StoreName })
                .Select(g => new TopStoreDto
                {
                    StoreKey = g.Key.StoreKey,
                    StoreName = g.Key.StoreName,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(5)
                .ToListAsync();

            dto.TopProducts = await _context.FactSales
                .Include(f => f.Product)
                .GroupBy(f => new { f.ProductKey, f.Product.ProductName })
                .Select(g => new TopProductDto
                {
                    ProductKey = g.Key.ProductKey,
                    ProductName = g.Key.ProductName,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(5)
                .ToListAsync();


            // =========================================
            // 6) ENTITY & USER COUNTS
            // =========================================

            dto.StoreCount = await _context.DimStores
                .CountAsync(s => s.IsActive);

            dto.ProductCount = await _context.DimProducts
                .CountAsync(p => p.IsActive);

            dto.CustomerCount = await _context.DimCustomers.CountAsync();

            dto.ActiveUserCount = await _context.UserAccounts
                .CountAsync(u => u.IsActive);

            return dto;
        }
    }
}

using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class StagingService : IStagingService
    {
        private readonly ProjectContext _context;

        public StagingService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<StagingPageDto> GetStagingOverviewAsync()
        {
            var now = DateTime.UtcNow;
            var last24h = now.AddHours(-24);

            var model = new StagingPageDto();

            // ========== ÜST METRİKLER ==========

            model.TotalRawRows = await _context.SalesRaws.LongCountAsync();

            model.LastLoadTime = await _context.SalesRaws
                .MaxAsync(r => (DateTime?)r.LoadedAt);

            if (model.LastLoadTime.HasValue)
            {
                model.DataFreshnessMinutes =
                    (int)Math.Round((now - model.LastLoadTime.Value).TotalMinutes);

                model.NewRowsLastLoad = await _context.SalesRaws
                    .LongCountAsync(r => r.LoadedAt == model.LastLoadTime.Value);
            }

            model.ErrorCountLast24h = await _context.EtlLogs
                .CountAsync(l => l.Level == Enums.LogLevel.Error &&
                                 l.LogTime >= last24h);

            // ========== RECENT ROWS GRID ==========

            model.RecentRows = await _context.SalesRaws
                .OrderByDescending(r => r.LoadedAt)
                .ThenByDescending(r => r.Id)
                .Take(8)
                .Select(r => new StagingRowDto
                {
                    Id = r.Id,
                    SalesTime = r.SalesTime,
                    StoreCode = r.StoreCode,
                    ProductCode = r.ProductCode,
                    Quantity = r.Quantity,
                    UnitPrice = r.UnitPrice,
                    LoadedAt = r.LoadedAt
                })
                .ToListAsync();

            // ========== SUMMARY CARD ==========

            var summary = new StagingSummaryDto
            {
                TotalRows = model.TotalRawRows,
                DistinctStores = await _context.SalesRaws
                    .Where(r => r.StoreCode != null)
                    .Select(r => r.StoreCode)
                    .Distinct()
                    .CountAsync(),
                DistinctProducts = await _context.SalesRaws
                    .Where(r => r.ProductCode != null)
                    .Select(r => r.ProductCode)
                    .Distinct()
                    .CountAsync(),
                MinSalesTime = await _context.SalesRaws.MinAsync(r => r.SalesTime),
                MaxSalesTime = await _context.SalesRaws.MaxAsync(r => r.SalesTime),
                MinLoadedAt = await _context.SalesRaws.MinAsync(r => (DateTime?)r.LoadedAt),
                MaxLoadedAt = model.LastLoadTime
            };

            // Ortalama quantity / unitprice
            summary.AvgQuantity = await _context.SalesRaws
                .Where(r => r.Quantity.HasValue)
                .AverageAsync(r => (double?)r.Quantity) ?? null;

            summary.AvgUnitPrice = await _context.SalesRaws
                .Where(r => r.UnitPrice.HasValue)
                .AverageAsync(r => (decimal?)r.UnitPrice) ?? 0m;

            model.Summary = summary;

            // ========== DATA QUALITY CARD ==========

            model.Quality = new StagingQualityDto
            {
                MissingStoreCodeCount = await _context.SalesRaws
                    .CountAsync(r => r.StoreCode == null),
                MissingProductCodeCount = await _context.SalesRaws
                    .CountAsync(r => r.ProductCode == null),
                InvalidQuantityCount = await _context.SalesRaws
                    .CountAsync(r => !r.Quantity.HasValue || r.Quantity <= 0),
                InvalidPriceCount = await _context.SalesRaws
                    .CountAsync(r => !r.UnitPrice.HasValue || r.UnitPrice <= 0)
            };

            // ========== LOAD TREND (LAST 7 DAYS, BAR CHART) ==========

            var fromDate = now.Date.AddDays(-6);   // son 7 gün
            model.LoadTrend = await _context.SalesRaws
                .Where(r => r.LoadedAt.Date >= fromDate)
                .GroupBy(r => r.LoadedAt.Date)
                .Select(g => new StagingLoadTrendPointDto
                {
                    Date = g.Key,
                    RowCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            // Eksik günler için sıfır dolduralım
            var trendDict = model.LoadTrend.ToDictionary(x => x.Date.Date, x => x);
            model.LoadTrend = new List<StagingLoadTrendPointDto>();
            for (int i = 0; i < 7; i++)
            {
                var d = fromDate.AddDays(i).Date;
                if (trendDict.TryGetValue(d, out var point))
                {
                    model.LoadTrend.Add(point);
                }
                else
                {
                    model.LoadTrend.Add(new StagingLoadTrendPointDto
                    {
                        Date = d,
                        RowCount = 0
                    });
                }
            }

            // ========== ERROR LOG CARD ==========

            model.ErrorLogs = await _context.EtlLogs
                .Where(l => l.Level != Enums.LogLevel.Info)
                .OrderByDescending(l => l.LogTime)
                .Take(6)
                .Select(l => new StagingErrorLogDto
                {
                    LogTime = l.LogTime,
                    Level = l.Level,
                    Message = l.Message
                })
                .ToListAsync();

            return model;
        }
    }
}

using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class DashboardSummaryDto
    {
        // ===============================
        // 1) TOP KPI METRICS
        // ===============================

        public decimal TotalSalesAmount { get; set; }
        public int TotalSalesQuantity { get; set; }

        public decimal TodaySalesAmount { get; set; }
        public int TodaySalesQuantity { get; set; }

        public decimal TodayVsYesterdaySalesPercent { get; set; }


        // ===============================
        // 2) DATA FRESHNESS
        // ===============================

        public DateTime? WarehouseLastLoadTime { get; set; }
        public int? WarehouseFreshnessHours { get; set; }

        public DateTime? StagingLastLoadTime { get; set; }
        public int? StagingFreshnessHours { get; set; }


        // ===============================
        // 3) ETL RUN SUMMARY
        // ===============================

        public EtlStatus? LastEtlStatus { get; set; }
        public DateTime? LastEtlStartTime { get; set; }
        public DateTime? LastEtlEndTime { get; set; }
        public string? LastEtlDurationText { get; set; }

        public int FailedRunsLast24Hours { get; set; }
        public int ActiveJobCount { get; set; }

        public List<RecentEtlRunDto> RecentRuns { get; set; } = new();


        // ===============================
        // 4) DAILY SALES CHART
        // ===============================

        // Grafikte kullanılan gün sayısı (7 / 14 / 30)
        public int SalesRangeDays { get; set; } = 14;

        public List<DailySalesPointDto> DailySales { get; set; } = new();


        // ===============================
        // 5) TOP STORES / PRODUCTS
        // ===============================

        public List<TopStoreDto> TopStores { get; set; } = new();
        public List<TopProductDto> TopProducts { get; set; } = new();


        // ===============================
        // 6) ENTITY COUNTS
        // ===============================

        public int StoreCount { get; set; }
        public int ProductCount { get; set; }
        public int CustomerCount { get; set; }
        public int ActiveUserCount { get; set; }
    }


    // ========== CHART POINT DTO ==========

    public class DailySalesPointDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }


    // ========== RECENT ETL RUN DTO ==========

    public class RecentEtlRunDto
    {
        public string JobName { get; set; } = string.Empty;
        public EtlStatus Status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string DurationText =>
            EndTime.HasValue
                ? (EndTime.Value - StartTime).ToString(@"hh\:mm\:ss")
                : "Running";
    }


    // ========== TOP STORES DTO ==========

    public class TopStoreDto
    {
        public int StoreKey { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }


    // ========== TOP PRODUCTS DTO ==========

    public class TopProductDto
    {
        public int ProductKey { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}

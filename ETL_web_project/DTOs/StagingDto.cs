using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    // Staging tablosundaki satırlar (üstteki küçük grid)
    public class StagingRowDto
    {
        public int Id { get; set; }
        public DateTime? SalesTime { get; set; }
        public string? StoreCode { get; set; }
        public string? ProductCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime LoadedAt { get; set; }
    }

    // Sağ alt hata log kartı
    public class StagingErrorLogDto
    {
        public DateTime LogTime { get; set; }
        public Enums.LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    // Load trend grafiği (bar chart)
    public class StagingLoadTrendPointDto
    {
        public DateTime Date { get; set; }
        public int RowCount { get; set; }
    }

    // Summary kartı (sol alttaki kart)
    public class StagingSummaryDto
    {
        public long TotalRows { get; set; }
        public int DistinctStores { get; set; }
        public int DistinctProducts { get; set; }

        public DateTime? MinSalesTime { get; set; }
        public DateTime? MaxSalesTime { get; set; }

        public DateTime? MinLoadedAt { get; set; }
        public DateTime? MaxLoadedAt { get; set; }

        public double? AvgQuantity { get; set; }
        public decimal? AvgUnitPrice { get; set; }
    }

    // Data Quality kartı için basit bayraklar
    public class StagingQualityDto
    {
        public int MissingStoreCodeCount { get; set; }
        public int MissingProductCodeCount { get; set; }
        public int InvalidQuantityCount { get; set; }
        public int InvalidPriceCount { get; set; }
    }

    // Sayfanın ana ViewModel’i
    public class StagingPageDto
    {
        // Üst metrikler
        public long TotalRawRows { get; set; }
        public long NewRowsLastLoad { get; set; }
        public DateTime? LastLoadTime { get; set; }
        public int? DataFreshnessMinutes { get; set; }
        public int ErrorCountLast24h { get; set; }

        public string SelectedTableName { get; set; } = "stg.SalesRaw";

        // Kartlar
        public List<StagingRowDto> RecentRows { get; set; } = new();
        public StagingSummaryDto Summary { get; set; } = new();
        public StagingQualityDto Quality { get; set; } = new();
        public List<StagingLoadTrendPointDto> LoadTrend { get; set; } = new();
        public List<StagingErrorLogDto> ErrorLogs { get; set; } = new();
    }
}

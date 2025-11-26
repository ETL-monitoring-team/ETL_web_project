using System;

namespace ETL_web_project.DTOs
{
    public class FactSummaryDto
    {
        public decimal TotalSales { get; set; }
        public int TotalQuantity { get; set; }
        public int DistinctStores { get; set; }
        public int DistinctProducts { get; set; }
        public int DistinctCustomers { get; set; }
        public decimal AvgOrderValue { get; set; }
    }

    public class FactTrendPointDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TopEntityDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }

    public class FactRecordDto
    {
        public DateTime Date { get; set; }
        public string Store { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// Fact Explorer sayfasının ViewModel'i
    /// </summary>
    public class FactExplorerPageDto
    {
        // Filtreler
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? StoreSearch { get; set; }
        public string? ProductSearch { get; set; }
        public string? CustomerSearch { get; set; }

        // İçerik
        public FactSummaryDto Summary { get; set; } = new();
        public List<FactTrendPointDto> Trend { get; set; } = new();
        public List<TopEntityDto> TopStores { get; set; } = new();
        public List<TopEntityDto> TopProducts { get; set; } = new();
        public List<TopEntityDto> TopCustomers { get; set; } = new();
        public List<FactRecordDto> Records { get; set; } = new();

        public List<SalesTrendPointDto> SalesTrend { get; set; } = new();
    }
}

public class TopItemDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}


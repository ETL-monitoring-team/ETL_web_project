namespace ETL_web_project.DTOs.FactExplorer
{
    public class FactExplorerPageDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? StoreSearch { get; set; }
        public string? ProductSearch { get; set; }
        public string? CustomerSearch { get; set; }
        public FactSummaryDto Summary { get; set; } = new();
        public List<TopEntityDto> TopStores { get; set; } = new();
        public List<TopEntityDto> TopProducts { get; set; } = new();
        public List<TopEntityDto> TopCustomers { get; set; } = new();
        public List<FactRecordDto> Records { get; set; } = new();
        public List<SalesTrendPointDto> SalesTrend { get; set; } = new();
    }
}

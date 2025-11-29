namespace ETL_web_project.DTOs.FactExplorer
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
}

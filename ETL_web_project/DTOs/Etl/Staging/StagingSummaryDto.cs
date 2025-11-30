namespace ETL_web_project.DTOs.Etl.Staging
{
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
}

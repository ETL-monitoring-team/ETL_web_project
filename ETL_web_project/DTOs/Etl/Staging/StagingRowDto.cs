namespace ETL_web_project.DTOs.Etl.Staging
{
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
}

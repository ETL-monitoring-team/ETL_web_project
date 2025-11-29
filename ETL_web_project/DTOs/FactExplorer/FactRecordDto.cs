namespace ETL_web_project.DTOs.FactExplorer
{
    public class FactRecordDto
    {
        public DateTime Date { get; set; }
        public string Store { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

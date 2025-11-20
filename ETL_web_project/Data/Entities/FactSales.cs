using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ETL_web_project.Data.Entities
{
    [Table("FactSales", Schema = "dw")]
    public class FactSales
    {
        [Key]
        public long SalesKey { get; set; }

        public int DateKey { get; set; }
        public int StoreKey { get; set; }
        public int ProductKey { get; set; }
        public int? CustomerKey { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(DateKey))]
        public virtual DimDate Date { get; set; }

        [ForeignKey(nameof(StoreKey))]
        public virtual DimStore Store { get; set; }

        [ForeignKey(nameof(ProductKey))]
        public virtual DimProduct Product { get; set; }

        [ForeignKey(nameof(CustomerKey))]
        public virtual DimCustomer Customer { get; set; }
    }
}

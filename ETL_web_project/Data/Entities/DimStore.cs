using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ETL_web_project.Data.Entities
{
    [Table("DimStore", Schema = "dw")]
    public class DimStore
    {
        [Key]
        public int StoreKey { get; set; }

        [Required, MaxLength(20)]
        public string StoreCode { get; set; }

        [Required, MaxLength(100)]
        public string StoreName { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        public bool IsActive { get; set; } = true;
    }
}


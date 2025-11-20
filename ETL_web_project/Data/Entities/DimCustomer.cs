using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("DimCustomer", Schema = "dw")]
    public class DimCustomer
    {
        [Key]
        public int CustomerKey { get; set; }

        [MaxLength(50)]
        public string CustomerCode { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(50)]
        public string City { get; set; }
    }
}

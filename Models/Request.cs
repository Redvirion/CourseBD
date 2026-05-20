using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseBD.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; } = DateTime.Today;

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseBD.Models
{
    public class ProductComponent
    {
        public int ProductId { get; set; }
        public int ComponentId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ComponentId")]
        public Component Component { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class ProductComposition
    {
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string ComponentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ComponentId")]
        public Component Component { get; set; }
    }
}
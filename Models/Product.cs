using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseBD.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        public int TechProcessId { get; set; }
        public string? ComponentsList { get; set; }

        [ForeignKey("TechProcessId")]
        public TechProcess TechProcess { get; set; }

        public ICollection<ProductComponent> ProductComponents { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
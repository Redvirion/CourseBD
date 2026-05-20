using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseBD.Models
{
    public class TechProcess
    {
        [Key]
        public int TechProcessId { get; set; }

        public int MaterialId { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal MaterialQuantity { get; set; }
     
        public string? OperationsList { get; set; }

        [ForeignKey("MaterialId")]
        public Material Material { get; set; }

        public ICollection<TechProcessOperation> TechProcessOperations { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
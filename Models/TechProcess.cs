using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class TechProcess
    {
        [Key]
        public int TechProcessId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ComponentId { get; set; }

        public int MaterialId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public decimal Quantity { get; set; }

        public int OperationId { get; set; }

        [ForeignKey("ComponentId")]
        public Component Component { get; set; }

        [ForeignKey("MaterialId")]
        public Material Material { get; set; }

        [ForeignKey("OperationId")]
        public Operation Operation { get; set; }
    }
}
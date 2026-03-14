using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Трудоёмкость не может быть отрицательной")]
        public decimal LaborHours { get; set; }

        public string? Composition { get; set; } // денормализованное поле (заполняется триггером)

        public int? MaterialRef { get; set; } // основной материал (необязательно)

        [ForeignKey("MaterialRef")]
        public Material? Material { get; set; }

        public ICollection<ProductComposition> ProductCompositions { get; set; }
    }
}
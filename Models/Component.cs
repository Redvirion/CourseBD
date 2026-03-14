using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class Component
    {
        [Key]
        [MaxLength(50)]
        public string ComponentId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Стоимость не может быть отрицательной")]
        public decimal Cost { get; set; }

        [MaxLength(50)]
        public string? TechProcessRef { get; set; } // ссылка на родительское комплектующее (для производимых)

        [ForeignKey("TechProcessRef")]
        public Component? ParentComponent { get; set; }
        public ICollection<Component> ChildComponents { get; set; }

        public ICollection<ProductComposition> ProductCompositions { get; set; }
        public ICollection<TechProcess> TechProcesses { get; set; }
    }
}
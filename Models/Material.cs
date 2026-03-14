using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        // Навигационное свойство
        public ICollection<TechProcess> TechProcesses { get; set; }
    }
}
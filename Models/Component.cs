using System.ComponentModel.DataAnnotations;

namespace CourseBD.Models
{
    public class Component
    {
        [Key]
        public int ComponentId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Стоимость не может быть отрицательной")]
        public decimal Cost { get; set; }

        public ICollection<ProductComponent> ProductComponents { get; set; }
    }
}
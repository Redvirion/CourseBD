using System.ComponentModel.DataAnnotations;

namespace CourseBD.Models
{
    public class Operation
    {
        [Key]
        public int OperationId { get; set; }

        [Required(ErrorMessage = "Наименование обязательно")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Стоимость не может быть отрицательной")]
        public decimal Cost { get; set; }

        public ICollection<TechProcessOperation> TechProcessOperations { get; set; }
    }
}
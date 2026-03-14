using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CourseBD.Models
{
    public class Operation
    {
        [Key]
        public int OperationId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Ставка не может быть отрицательной")]
        public decimal HourlyRate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Количество часов должно быть больше 0")]
        public decimal Hours { get; set; }

        public ICollection<TechProcess> TechProcesses { get; set; }
    }
}
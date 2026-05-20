using System.ComponentModel.DataAnnotations.Schema;

namespace CourseBD.Models
{
    public class TechProcessOperation
    {
        public int TechProcessId { get; set; }
        public int OperationId { get; set; }

        [ForeignKey("TechProcessId")]
        public TechProcess TechProcess { get; set; }

        [ForeignKey("OperationId")]
        public Operation Operation { get; set; }
    }
}
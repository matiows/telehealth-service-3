using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }

        public string MedicationName { get; set; }

        public string Description { get; set; }
    
        public string Unit { get; set; }
    }
}

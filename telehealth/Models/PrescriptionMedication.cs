using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace telehealth.Models
{
    public class PrescriptionMedication
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Key]
        public int MedicationId { get; set; }

        public int Quantity { get; set; } = 0;

        [JsonIgnore]
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }

        [JsonIgnore]
        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }

    }
}

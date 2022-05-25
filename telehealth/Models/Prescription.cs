using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        public int PrescribedById { get; set; }

        public int PrescribedToId { get; set; }

        public DateTime PresribeDate { get; set; } = DateTime.Now;

        public string? Remark { get; set; }

        public ICollection<PrescriptionMedication> PrescriptionMedications { get; set; }

    }
}

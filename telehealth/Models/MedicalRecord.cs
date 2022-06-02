using System.ComponentModel.DataAnnotations;

namespace telehealth.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }

        public int PatientId { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;

        public string Record { get; set; }

        public int PrescriptionId { get; set; } = 0;

    }
}

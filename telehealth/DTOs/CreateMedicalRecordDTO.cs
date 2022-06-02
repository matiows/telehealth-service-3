namespace telehealth.DTOs
{
    public class CreateMedicalRecordDTO
    {
        public int PatientId { get; set; }

        public string Record { get; set; }

        public int PrescriptionId { get; set; } = 0;

    }
}

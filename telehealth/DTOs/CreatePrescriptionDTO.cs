namespace telehealth.DTOs
{
    public class CreatePrescriptionDTO
    {

        public int PrescribedById { get; set; }

        public int PrescribedToId { get; set; }

        public string? Remark { get; set; }

        public ICollection<CreatePrescriptionMedication> PrescriptionMedications { get; set; }
    }

    public class CreatePrescriptionMedication
    {
        public int MedicationId { get; set; }

        public int Quantity { get; set; } = 0;

    }
}

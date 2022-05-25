using telehealth.Context;
using telehealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using telehealth.DTOs;
using telehealth.Services;

namespace telehealth.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PrescriptionController : Controller
    {
        private readonly DataContext context;

        private readonly IUserService _userService;

        public PrescriptionController(DataContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;

        }

        [HttpGet("{id}")]
        public ActionResult<Prescription> GetOne(int id)
        {
            var prescription = context.Prescriptions
                .Where(prescription => prescription.PrescriptionId == id)
                .Include(prescription => prescription.PrescriptionMedications)
                .FirstOrDefault();

            if (prescription == null) return NotFound("Prescription Not Found.");

            return Ok(prescription);
        }

        [HttpGet("patient")]
        public async Task<ActionResult<List<Prescription>>> GetByPatient(int id)
        {
            var prescriptions = await context.Prescriptions
                .Where(prescription => prescription.PrescribedToId == id)
                .Include(prescription => prescription.PrescriptionMedications)
                .ToListAsync();

            if (prescriptions == null) return NotFound("Prescription Not Found.");

            return Ok(prescriptions);
        }

        [HttpGet("doctor")]
        public async Task<ActionResult<List<Prescription>>> GetByDoctor(int id)
        {
            var prescriptions = await context.Prescriptions
                .Where(prescription => prescription.PrescribedById == id)
                .Include(prescription => prescription.PrescriptionMedications)
                .ToListAsync();

            if (prescriptions == null) return NotFound("Prescription Not Found.");

            return Ok(prescriptions);
        }

        [HttpGet]
        public async Task<ActionResult<List<Prescription>>> GetAll()
        {
            var prescriptions = await context.Prescriptions.ToListAsync();

            return Ok(prescriptions);
        }

        [HttpPost]
        public async Task<ActionResult<Prescription>> Post(CreatePrescriptionDTO prescriptionDTO)
        {
            Prescription prescription = new();
            await _userService.CheckUser(prescriptionDTO.PrescribedById);
            await _userService.CheckUser(prescriptionDTO.PrescribedToId);

            prescription.PrescribedById = prescriptionDTO.PrescribedById;
            prescription.PrescribedToId = prescriptionDTO.PrescribedToId;
            prescription.Remark = prescriptionDTO.Remark;

            ICollection<PrescriptionMedication> prescriptionMedications = new List<PrescriptionMedication>();

            foreach (var requestMedication in prescriptionDTO.PrescriptionMedications)
            {
                PrescriptionMedication prescriptionMedication = new();
                prescriptionMedication.Quantity = requestMedication.Quantity;

                var medication = context.Medications
                    .Where(medicine => medicine.MedicationId == requestMedication.MedicationId)
                    .FirstOrDefault();

                if (medication == null) return NotFound($"Medication with Id {requestMedication.MedicationId} Not Found");

                prescriptionMedication.MedicationId = requestMedication.MedicationId;

                prescriptionMedications.Add(prescriptionMedication);
            }

            prescription.PrescriptionMedications = prescriptionMedications;

            context.Prescriptions.Add(prescription);

            await context.SaveChangesAsync();

            return Ok(prescription);
        }
    }
}

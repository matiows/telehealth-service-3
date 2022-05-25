using telehealth.Context;
using telehealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using telehealth.DTOs;

namespace telehealth.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MedicationController : Controller
    {
        private readonly DataContext context;

        public MedicationController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Medication> GetOne(int id)
        {
            var medication = context.Medications
                .Where(medication => medication.MedicationId == id)
                .FirstOrDefault();

            if (medication == null) return NotFound("Medication Not Found.");

            return Ok(medication);
        }

        [HttpGet]
        public async Task<ActionResult<List<Medication>>> GetAll()
        {
            var medications = await context.Medications.ToListAsync();

            return Ok(medications);
        }

        [HttpPost]
        public async Task<ActionResult<Medication>> Post(CreateMedicationDTO medicationDTO)
        {
            Medication medication = new();
            medication.MedicationName = medicationDTO.MedicationName;
            medication.Description = medicationDTO.Description;
            medication.Unit = medicationDTO.Unit;

            context.Medications.Add(medication);

            await context.SaveChangesAsync();

            return Ok(medication);
        }

    }
}

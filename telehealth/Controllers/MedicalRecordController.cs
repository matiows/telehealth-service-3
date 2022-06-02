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
    public class MedicalRecordController : Controller
    {
        private readonly DataContext context;

        private readonly IUserService _userService;

        public MedicalRecordController(DataContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;
        }

        [HttpGet("id")]
        public ActionResult<MedicalRecord> GetOne(int id)
        {
            var record = context.MedicalRecords
                .Where(record => record.MedicalRecordId == id)
                .FirstOrDefault();

            if (record == null) return NotFound("Medical Record Not Found.");

            return Ok(record);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<MedicalRecord>>> GetByUser(int userId)
        {
            var records = await context.MedicalRecords
                .Where(record => record.PatientId == userId)
                .ToListAsync();

            if (records == null) return NotFound("No Medical Record Found.");

            return Ok(records);
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalRecord>>> GetAll()
        {
            var records = await context.MedicalRecords.ToListAsync();

            return Ok(records);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> Post(CreateMedicalRecordDTO recordDTO)
        {
            MedicalRecord record = new();
            await _userService.CheckUser(recordDTO.PatientId);
            record.PatientId = recordDTO.PatientId;
            record.Record = recordDTO.Record;
            record.PrescriptionId = recordDTO.PrescriptionId;

            context.MedicalRecords.Add(record);

            await context.SaveChangesAsync();

            return Ok(record);
        }

    }
}

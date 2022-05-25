using telehealth.Models;
using Microsoft.EntityFrameworkCore;

namespace telehealth.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<PrescriptionMedication> PrescriptionMedications { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Set Composite keys
            modelBuilder.Entity<PrescriptionMedication>()
                .HasKey(o => new { o.PrescriptionId, o.MedicationId });

        }
    }
}

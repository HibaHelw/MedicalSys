using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystemModule.MedicalContext
{
    public class MedicalContext : DbContext
    {
        public string _connectionString = "Data source=.\\SQLEXPRESS2014;Database=MedicalDB;Integrated Security=True;Trusted_Connection=True;";

        public MedicalContext(string connectionString)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Medical");
        }

        #region DBSets
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<DoctorClinicService> DoctorClinicServices { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        #endregion
    }
}

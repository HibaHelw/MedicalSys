using MedicalSystemModule.DTO;
using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Storage
{
    internal class ClinicStorage
    {
        private MedicalContext.MedicalContext _context;
        public ClinicStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public async Task<IEnumerable<IClinic>> GetAll()
        {
            return await _context.Clinics.Where(d => !d.DeletedAt.HasValue).Select(c => c.Transform()).ToListAsync();
        }

        public IClinic GetById(Guid id)
        {
            return _context.Clinics.FirstOrDefault(d => !d.DeletedAt.HasValue && d.Id == id)?.Transform();
        }

        public Guid CreateClinic(IClinic clinic)
        {
            var newClinic = new Clinic()
            {
                Name = clinic.Name,
                Location = clinic.Location,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Clinics.Add(newClinic);
            _context.SaveChanges();
            return newClinic.Id;
        }

        public void UpdateClinic(Guid id, IClinic clinic)
        {
            var clinicToUpdate = _context.Clinics.First(c => c.Id == id);
            clinicToUpdate.Name = clinic.Name;
            clinicToUpdate.Location = clinic.Location;
            clinicToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.Clinics.Update(clinicToUpdate);
            _context.SaveChanges();
        }

        public void DeleteClinic(Guid id)
        {
            var clinicToDelete = _context.Clinics.First(c => c.Id == id);
            clinicToDelete.DeletedAt = DateTime.UtcNow;
            _context.Clinics.Update(clinicToDelete);
            _context.SaveChanges();
        }

        public async Task<List<IDoctorClinicService>> GetClinicServices(Guid clinicId)
        {
            return await _context.DoctorClinicServices.Where(c => c.ClinicId == clinicId && !c.DeletedAt.HasValue)
                .Select(c => c.Transform()).ToListAsync();
        }
        public bool Exist(Guid id)
        {
            return _context.Clinics.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }
    }
}

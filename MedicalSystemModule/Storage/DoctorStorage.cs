using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Models;
using MedicalSystemModule.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Storage
{
    internal class DoctorStorage
    {
        private MedicalContext.MedicalContext _context;
        public DoctorStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public async Task<IEnumerable<IDoctor>> GetAll()
        {
            return await _context.Doctors.Where(d => !d.DeletedAt.HasValue).Select(c => c.Transform()).ToListAsync();
        }

        public IDoctor GetById(Guid id)
        {
            return _context.Doctors.FirstOrDefault(d => !d.DeletedAt.HasValue && d.Id == id)?.Transform();
        }

        public Guid CreateDoctor(IDoctor doctor)
        {
            var newDoc = new Doctor()
            {
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Doctors.Add(newDoc);
            _context.SaveChanges();
            return newDoc.Id;
        }

        public void UpdateDoctor(Guid id, IDoctor doctor)
        {
            var doctorToUpdate = _context.Doctors.First(c => c.Id == id);
            doctorToUpdate.Name = doctor.Name;
            doctorToUpdate.Specialty = doctor.Specialty;
            doctorToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.Doctors.Update(doctorToUpdate);
            _context.SaveChanges();
        }

        public void DeleteDoctor(Guid id)
        {
            var doctorToDelete = _context.Doctors.First(c => c.Id == id);
            doctorToDelete.DeletedAt = DateTime.UtcNow;
            _context.Doctors.Update(doctorToDelete);
            _context.SaveChanges();
        }

        public bool Exist(Guid id)
        {
            return _context.Doctors.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }

        public async Task<List<IDoctorClinicService>> GetDoctorServices(Guid doctorId)
        {
            return await _context.DoctorClinicServices.Where(c => c.DoctorId == doctorId && !c.DeletedAt.HasValue)
                .Select(c => c.Transform()).ToListAsync();
        }
    }
}

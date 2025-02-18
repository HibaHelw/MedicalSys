using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Models;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Storage
{
    internal class ClinicServicesStorage
    {
        private MedicalContext.MedicalContext _context;
        public ClinicServicesStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public IDoctorClinicService GetById(Guid id)
        {
            return _context.DoctorClinicServices.FirstOrDefault(c => c.Id == id && !c.DeletedAt.HasValue)?.Transform();
        }
        public Guid AddServiceToClinic(Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            var clinincService = new DoctorClinicService()
            {
                ClinicId = clinicId,
                ServiceId = ServiceId,
                DoctorId = DoctorId,
                CreatedAt = DateTime.UtcNow,
            };
            _context.DoctorClinicServices.Add(clinincService);
            _context.SaveChanges();
            return clinincService.Id;
        }

        public void UpdateServiceClinic(Guid id, Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            var clinincService =
                _context.DoctorClinicServices.FirstOrDefault(c => c.Id == id && !c.DeletedAt.HasValue);
            if (clinincService != null)
            {
                clinincService.DoctorId = DoctorId;
                clinincService.ClinicId = clinicId;
                clinincService.ServiceId = ServiceId;
                clinincService.UpdatedAt = DateTime.UtcNow;
                _context.DoctorClinicServices.Update(clinincService);
                _context.SaveChanges();
            }
        }

        public void DeleteClinicService(Guid id)
        {
            var clinincService =
                _context.DoctorClinicServices.FirstOrDefault(c => c.Id == id && !c.DeletedAt.HasValue);
            if (clinincService != null)
            {
                clinincService.DeletedAt = DateTime.UtcNow;
                _context.DoctorClinicServices.Update(clinincService);
                _context.SaveChanges();
            }
        }
        public bool Exist(Guid id)
        {
            return _context.DoctorClinicServices.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }
    }
}

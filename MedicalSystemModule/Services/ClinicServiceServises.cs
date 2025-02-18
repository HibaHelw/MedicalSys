using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Models;
using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces.Services;

namespace MedicalSystemModule.Services
{
    public class ClinicServiceServises:IClinicServiceServises
    {
        private ClinicServicesStorage storage;
        ClinicStorage clinicStorage;
        private ServiceStorage serviceStorage;
        private DoctorStorage doctorStorage;
        public ClinicServiceServises(IOptions<AppSettings> appsOptions)
        {
            storage = new ClinicServicesStorage(appsOptions);
            serviceStorage = new ServiceStorage(appsOptions);
            clinicStorage = new ClinicStorage(appsOptions);
            doctorStorage = new DoctorStorage(appsOptions);
        }
        public void AddServiceToClinicValidation(Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            if (!clinicStorage.Exist(clinicId)) throw new Exception("clinic Id doesn't exist");
            if (!serviceStorage.Exist(ServiceId)) throw new Exception("Service Id doesn't exist");
            if (DoctorId.HasValue && !doctorStorage.Exist(DoctorId.Value)) throw new Exception("Doctor Id doesn't exist");
        }
        public Guid AddServiceToClinic(Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            return storage.AddServiceToClinic(clinicId, ServiceId, DoctorId);
        }
        public void UpdateServiceClinicValidation(Guid id, Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            if (!storage.Exist(id)) throw new Exception("ClinicService Id doesn't exist");
            if (!clinicStorage.Exist(clinicId)) throw new Exception("clinic Id doesn't exist");
            if (!serviceStorage.Exist(ServiceId)) throw new Exception("Service Id doesn't exist");
            if (DoctorId.HasValue && !doctorStorage.Exist(DoctorId.Value)) throw new Exception("Doctor Id doesn't exist");
        }
        public void UpdateServiceClinic(Guid id, Guid clinicId, Guid ServiceId, Guid? DoctorId)
        {
            storage.UpdateServiceClinic(id, clinicId, ServiceId, DoctorId);
        }
        public void DeleteServiceClinicValidation(Guid id)
        {
            if (!storage.Exist(id)) throw new Exception("ClinicService Id doesn't exist");
            var clinicService = storage.GetById(id);
            if (clinicService.DoctorId.HasValue && !doctorStorage.GetDoctorServices(clinicService.DoctorId.Value).Result.
                    Any(c => c.ServiceId == clinicService.ServiceId || c.ClinicId == clinicService.ClinicId))
                throw new Exception("clinic service has relations with doctors, cannot be deleted");
        }
        public void DeleteClinicService(Guid id)
        {
            storage.DeleteClinicService(id);
        }
    }
}

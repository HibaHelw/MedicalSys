using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Models;
using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces.Services;

namespace MedicalSystemModule.Services
{
    public class DoctorServices:IDoctorServices
    {
        private DoctorStorage storage;
        public DoctorServices(IOptions<AppSettings> appsOptions)
        {
            storage = new DoctorStorage(appsOptions);
        }
        public async Task<IEnumerable<IDoctor>> GetAll()
        {
            return await storage.GetAll();
        }
        public IDoctor GetById(Guid id)
        {
            return storage.GetById(id);
        }

        public void CreateDoctorValidation(IDoctor doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.Name)) throw new Exception("Doctor name cannot be empty");
            if (string.IsNullOrWhiteSpace(doctor.Specialty)) throw new Exception("Doctor specialty cannot be empty");
        }

        public Guid CreateDoctor(IDoctor doctor)
        {
            return storage.CreateDoctor(doctor);
        }

        public void UpdateDoctorValidation(Guid id, IDoctor doctor)
        {
            if (!Exist(id)) throw new Exception("Doctor Id doesn't exist");
            if (string.IsNullOrWhiteSpace(doctor.Name)) throw new Exception("Doctor name cannot be empty");
            if (string.IsNullOrWhiteSpace(doctor.Specialty)) throw new Exception("Doctor specialty cannot be empty");
        }
        public void UpdateDoctor(Guid id, IDoctor doctor)
        {
            storage.UpdateDoctor(id, doctor);
        }

        public void DeleteDoctorValidation(Guid id)
        {
            if (!Exist(id)) throw new Exception("Doctor Id doesn't exist");
            var doctorServices = storage.GetDoctorServices(id);
            if (doctorServices.Result.Any())
                throw new Exception("Doctor has services related, cannot be deleted");
        }
        public void DeleteDoctor(Guid id)
        {
            storage.DeleteDoctor(id);
        }

        public bool Exist(Guid id)
        {
            return storage.Exist(id);
        }
    }
}

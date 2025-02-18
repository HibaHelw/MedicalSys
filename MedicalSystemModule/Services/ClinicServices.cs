using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Models;
using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Services
{
    public class ClinicServices : IClinicServices
    {
        private ClinicStorage storage;
        public ClinicServices(IOptions<AppSettings> appsOptions)
        {
            storage = new ClinicStorage(appsOptions);
        }
        public async Task<IEnumerable<IClinic>> GetAll()
        {
            return await storage.GetAll();
        }
        public IClinic GetById(Guid id)
        {
            return storage.GetById(id);
        }

        public void CreateClinicValidation(IClinic clinic)
        {
            if (string.IsNullOrWhiteSpace(clinic.Name)) throw new Exception("Clinic name cannot be empty");
            if (string.IsNullOrWhiteSpace(clinic.Location)) throw new Exception("Clinic location cannot be empty");
        }

        public Guid CreateClinic(IClinic clinic)
        {
            return storage.CreateClinic(clinic);
        }

        public void UpdateClinicValidation(Guid id, IClinic clinic)
        {
            if (!Exist(id)) throw new Exception("Clinic Id doesn't exist");
            if (string.IsNullOrWhiteSpace(clinic.Name)) throw new Exception("Clinic name cannot be empty");
            if (string.IsNullOrWhiteSpace(clinic.Location)) throw new Exception("Clinic location cannot be empty");
        }
        public void UpdateClinic(Guid id, IClinic clinic)
        {
            storage.UpdateClinic(id, clinic);
        }

        public void DeleteClinicValidation(Guid id)
        {
            if (!Exist(id)) throw new Exception("Clinic Id doesn't exist");
            var clinicServices = storage.GetClinicServices(id);
            if (clinicServices.Result.Any())
                throw new Exception("Clinic has services related, cannot be deleted");
        }
        public void DeleteClinic(Guid id)
        {
            storage.DeleteClinic(id);
        }

        public bool Exist(Guid id)
        {
            return storage.Exist(id);
        }
    }
}

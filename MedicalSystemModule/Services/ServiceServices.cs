using MedicalSystemModule.Interfaces;
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
    public class ServiceServices:IServiceServices
    {
        private ServiceStorage storage;
        private DoctorStorage doctorStorage;
        ClinicStorage clinicStorage;

        public ServiceServices(IOptions<AppSettings> appsOptions)
        {
            storage = new ServiceStorage(appsOptions);
            doctorStorage = new DoctorStorage(appsOptions);
            clinicStorage = new ClinicStorage(appsOptions);
        }
        public async Task<IEnumerable<IService>> GetAll()
        {
            return await storage.GetAll();
        }
        public IService GetById(Guid id)
        {
            return storage.GetById(id);
        }

        public void CreateServiceValidation(IService service)
        {
            if (string.IsNullOrWhiteSpace(service.Name)) throw new Exception("Service name cannot be empty");
            if (string.IsNullOrWhiteSpace(service.Description)) throw new Exception("Service description cannot be empty");
            if (service.Price < 0) throw new Exception("Price cannot be negative");
        }

        public Guid CreateService(IService service)
        {
            return storage.CreateService(service);
        }

        public void UpdateServiceValidation(Guid id, IService service)
        {
            if (!Exist(id)) throw new Exception("Service Id doesn't exist");
            if (string.IsNullOrWhiteSpace(service.Name)) throw new Exception("Service name cannot be empty");
            if (string.IsNullOrWhiteSpace(service.Description)) throw new Exception("Service description cannot be empty");
            if (service.Price < 0) throw new Exception("Price cannot be negative");
        }
        public void UpdateService(Guid id, IService service)
        {
            storage.UpdateService(id, service);
        }

        public void DeleteServiceValidation(Guid id)
        {
            if (!Exist(id)) throw new Exception("Service Id doesn't exist");
            var doctorServices = doctorStorage.GetDoctorServices(id);
            if (doctorServices.Result.Any())
                throw new Exception("service has doctors related, cannot be deleted");
            var clinicServices = clinicStorage.GetClinicServices(id);
            if (clinicServices.Result.Any())
                throw new Exception("service has clinics related, cannot be deleted");
        }
        public void DeleteService(Guid id)
        {
            storage.DeleteService(id);
        }

        public bool Exist(Guid id)
        {
            return storage.Exist(id);
        }
    }
}

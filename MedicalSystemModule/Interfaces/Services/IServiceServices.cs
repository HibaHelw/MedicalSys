using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Interfaces.Services
{
    public interface IServiceServices
    {
        public  Task<IEnumerable<IService>> GetAll();
        public IService GetById(Guid id);

        public void CreateServiceValidation(IService service);

        public Guid CreateService(IService service);
        public void UpdateServiceValidation(Guid id, IService service);
        public void UpdateService(Guid id, IService service);

        public void DeleteServiceValidation(Guid id);
        public void DeleteService(Guid id);

        public bool Exist(Guid id);
    }
}

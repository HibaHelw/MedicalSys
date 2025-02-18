using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Interfaces.Services
{
    public interface IDoctorServices
    {
        public Task<IEnumerable<IDoctor>> GetAll();
        public IDoctor GetById(Guid id);

        public void CreateDoctorValidation(IDoctor doctor);

        public Guid CreateDoctor(IDoctor doctor);

        public void UpdateDoctorValidation(Guid id, IDoctor doctor);
        public void UpdateDoctor(Guid id, IDoctor doctor);

        public void DeleteDoctorValidation(Guid id);
        public void DeleteDoctor(Guid id);

        public bool Exist(Guid id);
    }
}

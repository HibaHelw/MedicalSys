using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Interfaces.Services
{
    public interface IWorkingHoursServices
    {
        public IEnumerable<IWorkingHours> GetDoctorWorkingHours(Guid doctorId);
        public void CreateWorkingHoursValidation(IWorkingHours workingHours);

        public Guid CreateWorkingHours(IWorkingHours workingHours);
        public void UpdateDoctorWorkingHoursValidation(Guid id, IWorkingHours workingHours);
        public void UpdateDoctorWorkingHours(Guid id, IWorkingHours doctor);
        public void DeleteWorkingHoursValidation(Guid id);
        public void DeleteWorkingHours(Guid id);

        public bool Exist(Guid id);
    }
}

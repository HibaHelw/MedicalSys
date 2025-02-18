using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Interfaces.Services
{
    public interface IClinicServiceServises
    {
        public void AddServiceToClinicValidation(Guid clinicId, Guid ServiceId, Guid? DoctorId);
        public Guid AddServiceToClinic(Guid clinicId, Guid ServiceId, Guid? DoctorId);
        public void UpdateServiceClinicValidation(Guid id, Guid clinicId, Guid ServiceId, Guid? DoctorId);
        public void UpdateServiceClinic(Guid id, Guid clinicId, Guid ServiceId, Guid? DoctorId);
        public void DeleteServiceClinicValidation(Guid id);
        public void DeleteClinicService(Guid id);
    }
}

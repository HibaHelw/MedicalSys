using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class ClinicsResponse : IClinic
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }

        public static ClinicsResponse Transform(IClinic clinic)
        {
            return new ClinicsResponse
            {
                CreatedAt = clinic.CreatedAt,
                UpdatedAt = clinic.UpdatedAt,
                DeletedAt = clinic.DeletedAt,
                Name = clinic.Name,
                Location = clinic.Location,
                Id = clinic.Id,
                DoctorClinicServices = clinic.DoctorClinicServices
            };
        }
    }
}

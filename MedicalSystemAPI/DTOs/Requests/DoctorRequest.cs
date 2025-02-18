using System.Text.Json.Serialization;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Requests
{
    public class DoctorRequest : IDoctor
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        [JsonIgnore]
        public IEnumerable<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

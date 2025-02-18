using System.Text.Json.Serialization;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Requests
{
    public class WorkingHoursRequest:IWorkingHours
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        public Guid DoctorId { get; set; }
        [JsonIgnore]
        public IDoctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

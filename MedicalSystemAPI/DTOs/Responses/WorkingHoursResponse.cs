using MedicalSystemModule.Interfaces;

namespace MedicalSystemAPI.DTOs.Responses
{
    public class WorkingHoursResponse : IWorkingHours
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid DoctorId { get; set; }
        public IDoctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public static WorkingHoursResponse Transform(IWorkingHours workingHours)
        {
            return new WorkingHoursResponse()
            {
                CreatedAt = workingHours.CreatedAt,
                DeletedAt = workingHours.DeletedAt,
                Doctor = workingHours.Doctor,
                DoctorId = workingHours.DoctorId,
                EndTime = workingHours.EndTime,
                Id = workingHours.Id,
                StartTime = workingHours.StartTime,
                UpdatedAt = workingHours.UpdatedAt,
            };
        }
    }
}

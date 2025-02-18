using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MedicalSystemModule.Models
{
    public class WorkingHours : BaseModel
    {
        public IWorkingHours Transform()
        {
            return new TransformToWorkingHours()
            {
                Doctor = Doctor.Transform(),
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
                DoctorId = DoctorId,
                EndTime = EndTime,
                StartTime = StartTime,
            };
        }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

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
    public class DoctorClinicService : BaseModel
    {
        public IDoctorClinicService Transform()
        {
            return new TransformToDoctorClinicService()
            {
                Doctor = Doctor.Transform(),
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                DeletedAt = DeletedAt,
                DoctorId = DoctorId,
                Clinic = Clinic.Transform(),
                ClinicId = ClinicId,
                Id = Id,
                Service = Service.Transform(),
                ServiceId = ServiceId,
            };
        }
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public Guid ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}

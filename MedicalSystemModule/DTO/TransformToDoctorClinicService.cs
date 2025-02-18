using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemModule.DTO
{
    public class TransformToDoctorClinicService : IDoctorClinicService
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DoctorId { get; set; }
        public IDoctor? Doctor { get; set; }
        public Guid ClinicId { get; set; }
        public IClinic Clinic { get; set; }
        public Guid ServiceId { get; set; }
        public IService Service { get; set; }
    }
}

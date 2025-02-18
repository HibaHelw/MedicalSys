using MedicalSystemModule.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Interfaces
{
    public interface IDoctorClinicService : IBase
    {
        public Guid? DoctorId { get; set; }
        public IDoctor? Doctor { get; set; }
        public Guid ClinicId { get; set; }
        public IClinic Clinic { get; set; }
        public Guid ServiceId { get; set; }
        public IService Service { get; set; }
    }
}

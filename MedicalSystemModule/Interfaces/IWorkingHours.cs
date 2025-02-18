using MedicalSystemModule.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Interfaces
{
    public interface IWorkingHours : IBase
    {
        public Guid DoctorId { get; set; }
        public IDoctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

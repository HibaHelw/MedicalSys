using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Interfaces
{
    public interface IDoctor : IBase
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public IEnumerable<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

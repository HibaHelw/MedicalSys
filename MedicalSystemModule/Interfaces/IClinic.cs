using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Interfaces
{
    public interface IClinic : IBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

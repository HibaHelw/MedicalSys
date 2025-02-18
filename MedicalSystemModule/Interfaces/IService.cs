using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystemModule.Interfaces
{
    public interface IService : IBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

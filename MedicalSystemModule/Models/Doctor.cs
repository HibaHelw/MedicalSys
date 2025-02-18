using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.DTO;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemModule.Models
{
    public class Doctor : BaseModel
    {
        public IDoctor Transform()
        {
            return new TransformToDoctor()
            {
                Name = Name,
                Specialty = Specialty,
                DoctorClinicServices = DoctorClinicServices,
            };
        }
        public string Name { get; set; }
        public string Specialty { get; set; }
        [NotMapped]
        public IEnumerable<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

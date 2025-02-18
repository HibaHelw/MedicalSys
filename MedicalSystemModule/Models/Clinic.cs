using MedicalSystemModule.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemModule.Models
{
    public class Clinic : BaseModel
    {
        public IClinic Transform()
        {
            return new TransformToClinic()
            {
                Name = Name,
                Location = Location,
                DoctorClinicServices = DoctorClinicServices,
            };
        }
        public string Name { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public ICollection<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;

namespace MedicalSystemModule.DTO
{
    public class TransformToDoctor : IDoctor
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public IEnumerable<IDoctorClinicService> DoctorClinicServices { get; set; }
    }
}

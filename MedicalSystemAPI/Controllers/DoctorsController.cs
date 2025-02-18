using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DoctorsController : ControllerBase
    {
        private DoctorServices service;

        public DoctorsController(IOptions<AppSettings> appsOptions)
        {
            service = new DoctorServices(appsOptions);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "جلب كل الأطباء")]
        public async Task<IEnumerable<DoctorsResponse>> GetAllDoctors()
        {
            return service.GetAll().Result.Select(c => DoctorsResponse.Transform(c));
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "جلب طبيب معين")]
        public DoctorsResponse GeTDoctorById(Guid id)
        {
            return DoctorsResponse.Transform(service.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "إضافة طبيب إلى النظام")]
        public Guid CreateDoctor([FromBody] DoctorRequest doctor)
        {
            service.CreateDoctorValidation(doctor);
            return service.CreateDoctor(doctor);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "تعديل طبيب في النظام")]
        public void UpdateDoctor(Guid id, [FromBody] DoctorRequest doctor)
        {
            service.UpdateDoctorValidation(id, doctor);
            service.UpdateDoctor(id, doctor);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "حذف طبيب من النظام")]
        public void DeleteDoctor(Guid id)
        {
            service.DeleteDoctorValidation(id);
            service.DeleteDoctor(id);
        }
    }
}

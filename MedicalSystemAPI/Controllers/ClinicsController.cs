using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.Interfaces.Services;
using MedicalSystemModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicsController : ControllerBase
    {

        private IClinicServices service;

        public ClinicsController(IClinicServices clinicSer)
        {
            service = clinicSer;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "جلب كل العيادات")]
        public async Task<IEnumerable<ClinicsResponse>> GetAll()
        {
            return service.GetAll().Result.Select(c => ClinicsResponse.Transform(c));
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "جلب عيادة معينة")]
        public ClinicsResponse GeTById(Guid id)
        {
            return ClinicsResponse.Transform(service.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "إضافة عيادة إلى النظام")]
        public Guid Create([FromBody] ClinicRequest clinic)
        {
            service.CreateClinicValidation(clinic);
            return service.CreateClinic(clinic);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "تعديل عيادة في النظام")]
        public void Update(Guid id, [FromBody] ClinicRequest clinic)
        {
            service.UpdateClinicValidation(id, clinic);
            service.UpdateClinic(id, clinic);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "حذف عيادة من النظام")]
        public void Delete(Guid id)
        {
            service.DeleteClinicValidation(id);
            service.DeleteClinic(id);
        }
    }
}

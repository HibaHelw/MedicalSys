using MedicalSystemAPI.DTOs.Requests;
using MedicalSystemAPI.DTOs.Responses;
using MedicalSystemModule.MedicalContext;
using MedicalSystemModule.Services;
using MedicalSystemModule.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private ServiceServices _service;
        private ClinicServiceServises clinicService;

        public ServicesController(IOptions<AppSettings> appsOptions)
        {
            _service = new ServiceServices(appsOptions);
            clinicService = new ClinicServiceServises(appsOptions);
        }

        #region Services

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "جلب كل الخدمات")]
        public async Task<IEnumerable<ServicesResponse>> GetAll()
        {
            return _service.GetAll().Result.Select(c => ServicesResponse.Transform(c));
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "جلب خدمة معينة")]
        public ServicesResponse GeTById(Guid id)
        {
            return ServicesResponse.Transform(_service.GetById(id));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "إضافة خدمة إلى النظام")]
        public Guid Create([FromBody] ServiceRequest service)
        {
            _service.CreateServiceValidation(service);
            return _service.CreateService(service);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "تعديل خدمة في النظام")]
        public void Update(Guid id, [FromBody] ServiceRequest service)
        {
            _service.UpdateServiceValidation(id, service);
            _service.UpdateService(id, service);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "حذف خدمة من النظام")]
        public void Delete(Guid id)
        {
            _service.DeleteServiceValidation(id);
            _service.DeleteService(id);
        }

        #endregion

        #region clinic Services

        [HttpPost("ClinicService")]
        [Authorize]
        [SwaggerOperation(Summary = "إضافة خدمة إلى عيادة معينة")]
        public Guid AddServiceToClinic(Guid clinicId, Guid serviceId, Guid? doctorId)
        {
            clinicService.AddServiceToClinicValidation(clinicId, serviceId, doctorId);
            return clinicService.AddServiceToClinic(clinicId, serviceId, doctorId);
        }

        [HttpPut("ClinicService/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "تعديل خدمة في عيادة معينة")]
        public void UpdateServiceToClinic(Guid id, Guid clinicId, Guid serviceId, Guid? doctorId)
        {
            clinicService.UpdateServiceClinicValidation(id, clinicId, serviceId, doctorId);
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, doctorId);
        }

        [HttpDelete("ClinicService/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "حذف خدمة من عيادة معينة")]
        public void DeleteServiceFromClinic(Guid id)
        {
            clinicService.DeleteServiceClinicValidation(id);
            clinicService.DeleteClinicService(id);
        }

        #endregion

        #region DoctorServices

        [HttpPut("DoctorService/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "تعديل خدمة إلى طبيب معين")]
        public void UpdateDoctorService(Guid id, Guid clinicId, Guid serviceId, Guid doctorId)
        {
            clinicService.UpdateServiceClinicValidation(id, clinicId, serviceId, doctorId);
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, doctorId);
        }

        [HttpDelete("DoctorService/{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "حذف خدمة من طبيب معين")]
        public void DeleteServiceFromDoctor(Guid id, Guid clinicId, Guid serviceId)
        {
            clinicService.UpdateServiceClinicValidation(id, clinicId, serviceId, null);
            clinicService.UpdateServiceClinic(id, clinicId, serviceId, null);
        }
        #endregion


    }
}

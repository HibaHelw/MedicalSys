using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Storage;
using MedicalSystemModule.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces.Services;

namespace MedicalSystemModule.Services
{
    public class WorkingHoursServices : IWorkingHoursServices
    {
        private WorkingHoursStorage storage;
        private DoctorStorage doctorStorage;
        public WorkingHoursServices(IOptions<AppSettings> appsOptions)
        {
            storage = new WorkingHoursStorage(appsOptions);
            doctorStorage = new DoctorStorage(appsOptions);
        }

        public IEnumerable<IWorkingHours> GetDoctorWorkingHours(Guid doctorId)
        {
            return storage.GetDoctorWorkingHours(doctorId);
        }
        public void CreateWorkingHoursValidation(IWorkingHours workingHours)
        {
            if (workingHours.StartTime >= workingHours.EndTime)
                throw new Exception("Start time must be less than end time");
            if (!doctorStorage.Exist(workingHours.DoctorId))
                throw new Exception("Doctor id doesn't exist");
        }

        public Guid CreateWorkingHours(IWorkingHours workingHours)
        {
            return storage.AddWorkingHoursForDoctor(workingHours);
        }

        public void UpdateDoctorWorkingHoursValidation(Guid id, IWorkingHours workingHours)
        {
            if (!Exist(id)) throw new Exception("Doctor Id doesn't exist");
            if (workingHours.StartTime >= workingHours.EndTime)
                throw new Exception("Start time must be less than end time");
            if (!doctorStorage.Exist(workingHours.DoctorId))
                throw new Exception("Doctor id doesn't exist");
        }
        public void UpdateDoctorWorkingHours(Guid id, IWorkingHours doctor)
        {
            storage.UpdateTimeSlot(id, doctor);
        }

        public void DeleteWorkingHoursValidation(Guid id)
        {
            if (!Exist(id)) throw new Exception("Doctor Id doesn't exist");
        }
        public void DeleteWorkingHours(Guid id)
        {
            storage.DeleteWorkingHour(id);
        }

        public bool Exist(Guid id)
        {
            return storage.Exist(id);
        }
    }
}

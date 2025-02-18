using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using MedicalSystemModule.Models;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Storage
{
    internal class WorkingHoursStorage
    {
        private MedicalContext.MedicalContext _context;
        public WorkingHoursStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public IEnumerable<IWorkingHours> GetDoctorWorkingHours(Guid doctorId)
        {
            return _context.WorkingHours.Where(w => w.DoctorId == doctorId && !w.DeletedAt.HasValue)
                .Select(c => c.Transform());
        }

        public Guid AddWorkingHoursForDoctor(IWorkingHours workingHours)
        {
            var DocWorkingHours = new WorkingHours()
            {
                DoctorId = workingHours.DoctorId,
                StartTime = workingHours.StartTime,
                EndTime = workingHours.EndTime,
            };
            _context.WorkingHours.Add(DocWorkingHours);
            _context.SaveChanges();
            return DocWorkingHours.Id;
        }

        public void UpdateTimeSlot(Guid id, IWorkingHours workingHours)
        {
            var DocWorkingHour = _context.WorkingHours.FirstOrDefault(c => c.Id == id && !c.DeletedAt.HasValue);
            if (DocWorkingHour != null)
            {
                DocWorkingHour.DoctorId = workingHours.DoctorId;
                DocWorkingHour.StartTime = workingHours.StartTime;
                DocWorkingHour.EndTime = workingHours.EndTime;
                DocWorkingHour.UpdatedAt = DateTime.UtcNow;
                _context.WorkingHours.Update(DocWorkingHour);
                _context.SaveChanges();
            }
        }

        public void DeleteWorkingHour(Guid id)
        {
            var workingHourToDelete = _context.WorkingHours.First(c => c.Id == id);
            workingHourToDelete.DeletedAt = DateTime.UtcNow;
            _context.WorkingHours.Update(workingHourToDelete);
            _context.SaveChanges();
        }

        public bool Exist(Guid id)
        {
            return _context.WorkingHours.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }
    }
}

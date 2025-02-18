using MedicalSystemModule.DTO;
using MedicalSystemModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalSystemModule.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedicalSystemModule.Utilities;
using Microsoft.Extensions.Options;

namespace MedicalSystemModule.Storage
{
    internal class ServiceStorage
    {
        private MedicalContext.MedicalContext _context;
        public ServiceStorage(IOptions<AppSettings> appsOptions)
        {
            _context = new MedicalContext.MedicalContext(appsOptions.Value.ConnectionString);
        }

        public async Task<IEnumerable<IService>> GetAll()
        {
            return await _context.Services.Where(d => !d.DeletedAt.HasValue).Select(c => c.Transform()).ToListAsync();
        }

        public IService GetById(Guid id)
        {
            return _context.Services.FirstOrDefault(d => !d.DeletedAt.HasValue && d.Id == id)?.Transform();
        }

        public Guid CreateService(IService service)
        {
            var newService = new Service()
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                CreatedAt = DateTime.UtcNow,
            };
            _context.Services.Add(newService);
            _context.SaveChanges();
            return newService.Id;
        }

        public void UpdateService(Guid id, IService service)
        {
            var serviceToUpdate = _context.Services.First(c => c.Id == id);
            serviceToUpdate.Name = service.Name;
            serviceToUpdate.Description = service.Description;
            serviceToUpdate.Price = service.Price;
            serviceToUpdate.UpdatedAt = DateTime.UtcNow;
            _context.Services.Update(serviceToUpdate);
            _context.SaveChanges();
        }

        public void DeleteService(Guid id)
        {
            var serviceToDelete = _context.Services.First(c => c.Id == id);
            serviceToDelete.DeletedAt = DateTime.UtcNow;
            _context.Services.Update(serviceToDelete);
            _context.SaveChanges();
        }

        public bool Exist(Guid id)
        {
            return _context.Services.Any(c => c.Id == id && !c.DeletedAt.HasValue);
        }
    }
}

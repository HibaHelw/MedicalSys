namespace MedicalSystemModule.Interfaces.Services
{
    public interface IClinicServices
    {
        public Task<IEnumerable<IClinic>> GetAll();
        public IClinic GetById(Guid id);
        public void CreateClinicValidation(IClinic clinic);

        public Guid CreateClinic(IClinic clinic);

        public void UpdateClinicValidation(Guid id, IClinic clinic);
        public void UpdateClinic(Guid id, IClinic clinic);

        public void DeleteClinicValidation(Guid id);
        public void DeleteClinic(Guid id);

        public bool Exist(Guid id);
    }
}

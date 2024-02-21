using HealthRecord.Domain.Interfaces;
using HealthRecord.Domain.Models;
using HealthRecord.Infrastructure.Services;

namespace HealthRecord.Infrastructure
{
    public class PatientRecordService : ICrud
    {
        private readonly IDataService<Patient> _crudServices;

        public PatientRecordService()
        {
            _crudServices = new GenericDataService<Patient>(new PatientContextFactory());
        }

        public async Task<Patient> AddBrand(string name, string course)
        {
            try
            {
                if (name == string.Empty)
                {
                    throw new Exception("Patient Name Cannot be Empty");
                }
                else
                {
                    Patient br = new Patient
                    {
                        Name = name,
                        Details = course
                    };
                    return await _crudServices.Create(br);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBrand(int id)
        {
            try
            {
                Patient delete = await SearchBrandbyID(id);

                return await _crudServices.Delete(delete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<Patient>> ListBrands()
        {
            try
            {
                return (ICollection<Patient>)await _crudServices.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Task<Patient> SearchBrandbyID(int ID)
        {
            try
            {
                return _crudServices.Get(ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Patient>> SearchBrandByName(string name)
        {
            try
            {
                var listbrand = await ListBrands();
                return listbrand.Where(x => x.Name.StartsWith(name)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<Patient> UpdateBrand(int id, string name, string details)
        {
            try
            {
                Patient br = await SearchBrandbyID(id);
                br.Name = name;
                br.Details = details;
                return await _crudServices.Update(br);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

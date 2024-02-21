namespace HealthRecord.Infrastructure.Services
{
    using HealthRecord.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GenericDataService<T> : IDataService<T> where T : class
    {
        private readonly PatientContextFactory _contextFactory;
        private readonly NonqueryDataService<T> _nonQueryDataService;

        public GenericDataService(PatientContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonqueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public async Task<T> Get(int id)
        {
            using PatientDbContext context = _contextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using PatientDbContext context = _contextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            return await _nonQueryDataService.Update(entity);
        }
    }
}

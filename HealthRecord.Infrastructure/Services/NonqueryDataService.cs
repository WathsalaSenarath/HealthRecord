using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecord.Infrastructure.Services
{
    public class NonqueryDataService<T> where T : class
    {
        private readonly PatientContextFactory _contextFactory;

        public NonqueryDataService(PatientContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<T> Create(T entity)
        {
            using PatientDbContext context = _contextFactory.CreateDbContext();
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return createdResult.Entity;
        }

        public async Task<bool> Delete(T entity)
        {
            using PatientDbContext context = _contextFactory.CreateDbContext();
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<T> Update(T entity)
        {
            using PatientDbContext context = _contextFactory.CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

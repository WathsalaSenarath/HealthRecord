using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecord.Infrastructure
{
    public class PatientContextFactory : IDesignTimeDbContextFactory<PatientDbContext>
    {
        public PatientDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<PatientDbContext>();
            options.UseSqlServer("server=.\\sqlexpress;database=;Trusted_Connection=true;Encrypt=False");
            return new PatientDbContext(options.Options);
        }
    }
}

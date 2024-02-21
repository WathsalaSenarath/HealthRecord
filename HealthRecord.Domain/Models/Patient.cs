using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecord.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
    }
}

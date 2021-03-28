using Microsoft.EntityFrameworkCore;
using Mutant.Models.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutant.Models.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<DnaRecord> DnaRecords { get; set; }
    }
}

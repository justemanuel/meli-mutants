using Microsoft.EntityFrameworkCore;
using Mutant.Models.Database.Entity;

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

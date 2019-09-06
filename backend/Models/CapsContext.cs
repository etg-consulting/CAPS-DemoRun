using Microsoft.EntityFrameworkCore;

namespace Caps.Models
{
    public class CapsContext : DbContext
    {
        public CapsContext(DbContextOptions<CapsContext> options)
            : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
    }
}
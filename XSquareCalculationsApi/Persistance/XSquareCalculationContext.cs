using Microsoft.EntityFrameworkCore;
using XSquareCalculationsApi.Entities;

namespace XSquareCalculationsApi.Persistance
{
    public class XSquareCalculationContext : DbContext
    {
        public XSquareCalculationContext(DbContextOptions<XSquareCalculationContext> options)
            : base(options)
        {
        }

        public DbSet<Template> Templates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Authenticate> Authenticates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

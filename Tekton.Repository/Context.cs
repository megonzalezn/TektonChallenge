using Microsoft.EntityFrameworkCore;
using Tekton.Entity;

namespace Tekton.Repository
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions options) : base(options) { }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            
        }
    }
}

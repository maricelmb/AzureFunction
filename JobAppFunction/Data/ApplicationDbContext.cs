using JobAppFunction.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAppFunction.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public  DbSet<SalesRequest> SalesRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesRequest>(entity => entity.HasKey (e => e.Id));
        }
    }
}

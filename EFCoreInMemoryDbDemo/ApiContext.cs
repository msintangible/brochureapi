using brochureapi.Models;
using Microsoft.EntityFrameworkCore;


namespace brochureapi.EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
     
       

        public ApiContext(DbContextOptions<ApiContext> options)
           : base(options)
        {
        }
       

        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Page> Pages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brochure>()
                .HasMany(b => b.Pages)
                 .WithOne(b => b.Brochure)
                 .HasForeignKey(b => b.BrochureId).IsRequired(false);
        }
    }
}
